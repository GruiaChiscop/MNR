using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio;
using System.Security.Cryptography;

namespace WpfApp1
{
    public class TextToMorse
                {
        private int _charSpeed;
        public int CharSpeed
        {
            get
            {
                return _charSpeed;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Speed can't be 0 or negative");
                }
                _charSpeed = value;
            }
        }

        public enum SignalType
        {
            Sin = SignalGeneratorType.Sin,
            Square = SignalGeneratorType.Square,
            Sawtoot = SignalGeneratorType.SawTooth,
            Triangle = SignalGeneratorType.Triangle
        }

        private int _pitch;
        public int Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Pitch can't be 0 or negative");
                }
                _pitch = value;
                dot.Frequency= _pitch;
                dash.Frequency= _pitch;
            }
        }

        private int _wordSpeed;
        public int WordSpeed
        {
            get
            {
                return _wordSpeed;
            }
            set
            {
                if (value < CharSpeed)
                {
                    throw new ArgumentException("Word speed must be greater or equal to character speed");
                }
                _wordSpeed = value;
            }
        }
        private SignalType s;
        public SignalType Type
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
                dot.Type = (SignalGeneratorType)value;
                dash.Type = (SignalGeneratorType)value;
            }
            }

        private SignalGenerator dot;
        private SignalGenerator dash;
        private WaveOutEvent waveOut;
        public TextToMorse(int charSpeed, int wordSpeed, int pitch, SignalType signalType)
        {
            _wordSpeed = wordSpeed;
            _charSpeed = charSpeed;
            _pitch = pitch;
            s = signalType;
            dot = new SignalGenerator { Frequency = Pitch, Gain = 0.5, Type = (SignalGeneratorType)signalType};
            dash = new SignalGenerator { Frequency = Pitch, Gain = 0.5, Type = (SignalGeneratorType)signalType };
            waveOut = new WaveOutEvent();
                    }

        public TextToMorse() : this(20, 20, 500, TextToMorse.SignalType.Triangle) { }

        private double dotLength
        {
            get
            {
                return 1.2 / CharSpeed;
                //return 60000 / CharSpeed;
            }
        }
        private double dashLength
        {
            get
            {
                return 3.6 / CharSpeed;
                //return 60000 / CharSpeed / 3;
            }
        }
        private double innerCharSilence
        {
            get
            {
                return dotLength;
            }
        }
        private double charSilence
        {
            get
            {
                double delay = 60.0 / WordSpeed - 32.0 / CharSpeed;
                return 3 * delay / 19;
            }
        }
        private double wordSilence
        {
            get
            {
                double delay = 60.0 / WordSpeed - 32.0 / CharSpeed;
                return 7 * delay / 19;
            }
        }
private ISampleProvider GetDot()
        {
            var dot = this.dot.Take(TimeSpan.FromSeconds(dotLength));
            return dot;
        }

        private ISampleProvider GetDash()
        {
            var dash = this.dash.Take(TimeSpan.FromSeconds(dashLength));
            return dash;
        }

        private ISampleProvider GetInterElementSilence()
        {
            var s = new SignalGenerator { Frequency=0, Gain=0 }.Take(TimeSpan.FromSeconds(dotLength));
            return s;
        }

        private ISampleProvider GetInterCharSilence()
        {
            var s = new SignalGenerator { Frequency=0, Gain=0}.Take(TimeSpan.FromSeconds(charSilence));
        return s;
        }

        private ISampleProvider GetWordSilence()
        {
        var s = new SignalGenerator { Frequency=0, Gain=0 }.Take(TimeSpan.FromSeconds(wordSilence));
            return s;
        }

        private ISampleProvider GetChar(char ch)
        {
            List<ISampleProvider> buffer = new List<ISampleProvider>();
            if (Characters.Symbols.ContainsKey(ch))
            {
                foreach (var character in Characters.Symbols[ch])
                {
                    if (character == '.')
                    {
                        buffer.Add(GetDot());
                    }
                    else if (character == '-')
                    {
                        buffer.Add(GetDash());
                    }
                    buffer.Add(GetInterElementSilence());
                }
            }
                                    var concatenated = new ConcatenatingSampleProvider(buffer);
            var ret = concatenated.FollowedBy(GetInterCharSilence());
            return ret;
        }

        private ISampleProvider GetWord(string word)
        {
            var buffer = new List<ISampleProvider>();
            foreach (char ch in word)
            {
                buffer.Add(GetChar(ch));
            }
            var sample = new ConcatenatingSampleProvider(buffer);
            var c = sample.FollowedBy(GetWordSilence());
            return c;
        }

        public ISampleProvider GetFullText(string text)
        {
            string[] words = text.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var buffer = new List<ISampleProvider>();
            foreach (var word in words)
            {
                buffer.Add(GetWord(word));
            }

            var wholeBuffer = new ConcatenatingSampleProvider(buffer);
            var ret = wholeBuffer.FollowedBy(GetWordSilence());
            return ret;
        }

        public async Task Transmit(string text)
        {
            ISampleProvider fullTextSample = GetFullText(text.ToLower());
            var waveprovidertext = fullTextSample.ToWaveProvider();
                waveOut.Init(fullTextSample);
                waveOut.Volume = 0.3f;
                    waveOut.Play();

                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(500);
                    //System.Threading.Thread.Sleep(500);
                }
            //await Task.Delay(5000);
            waveOut.Dispose();
                    }

        public bool IsPlaying
        {
            get
            {
                return waveOut.PlaybackState == PlaybackState.Playing;
            }
        }
        public void Stop()
        {
            if(IsPlaying)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
        }
    }
}
