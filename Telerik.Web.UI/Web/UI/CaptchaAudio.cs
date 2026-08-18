using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Speech.Synthesis;

namespace Telerik.Web.UI
{
	// Token: 0x020016D1 RID: 5841
	public class CaptchaAudio
	{
		// Token: 0x17004533 RID: 17715
		// (get) Token: 0x0600E1A5 RID: 57765 RVA: 0x00322986 File Offset: 0x00320B86
		protected MemoryStream AudioMemoryStream
		{
			get
			{
				return this._audioMemoryStream;
			}
		}

		// Token: 0x17004534 RID: 17716
		// (get) Token: 0x0600E1A6 RID: 57766 RVA: 0x0032298E File Offset: 0x00320B8E
		internal string TextToSpeak
		{
			get
			{
				return this._textToSpeak;
			}
		}

		// Token: 0x17004535 RID: 17717
		// (get) Token: 0x0600E1A7 RID: 57767 RVA: 0x00322996 File Offset: 0x00320B96
		// (set) Token: 0x0600E1A8 RID: 57768 RVA: 0x0032299E File Offset: 0x00320B9E
		internal bool CanSpeak
		{
			get
			{
				return this._canSpeak;
			}
			set
			{
				this._canSpeak = value;
			}
		}

		// Token: 0x0600E1A9 RID: 57769 RVA: 0x003229A8 File Offset: 0x00320BA8
		public CaptchaAudio(MemoryStream audioMemoryStream, string textToSpeak)
		{
			this._audioMemoryStream = audioMemoryStream;
			this._textToSpeak = textToSpeak;
		}

		// Token: 0x0600E1AA RID: 57770 RVA: 0x00322C20 File Offset: 0x00320E20
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void SpeakText()
		{
			this.CanSpeak = false;
			try
			{
				SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
				speechSynthesizer.SetOutputToWaveStream(this.AudioMemoryStream);
				int length = this.TextToSpeak.Length;
				speechSynthesizer.Rate = -4;
				for (int i = 0; i < length; i++)
				{
					speechSynthesizer.Speak(this.wordsToSpeak[this.TextToSpeak[i].ToString().ToUpper(CultureInfo.InvariantCulture)]);
				}
				this.CanSpeak = true;
			}
			catch (SecurityException)
			{
				this.CanSpeak = false;
			}
			catch (ArgumentException)
			{
				this.CanSpeak = false;
			}
			catch (InvalidOperationException)
			{
				this.CanSpeak = false;
			}
			catch (Exception)
			{
				this.CanSpeak = false;
			}
		}

		// Token: 0x0600E1AB RID: 57771 RVA: 0x00322CFC File Offset: 0x00320EFC
		public MemoryStream GetWaveStream(string currentApplicationPath)
		{
			string[] array = new string[this.TextToSpeak.Length];
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = currentApplicationPath + "\\" + this.TextToSpeak[i].ToString().ToUpper() + ".wav";
			}
			MemoryStream result = new MemoryStream();
			try
			{
				result = CaptchaCombineAudio.Concatenate(array);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x04004164 RID: 16740
		private readonly MemoryStream _audioMemoryStream;

		// Token: 0x04004165 RID: 16741
		private readonly string _textToSpeak;

		// Token: 0x04004166 RID: 16742
		private bool _canSpeak = true;

		// Token: 0x04004167 RID: 16743
		private readonly Dictionary<string, string> wordsToSpeak = new Dictionary<string, string>
		{
			{
				"A",
				"Alpha"
			},
			{
				"B",
				"Bravo"
			},
			{
				"C",
				"Charlie"
			},
			{
				"D",
				"Delta"
			},
			{
				"E",
				"Echo"
			},
			{
				"F",
				"Foxtrot"
			},
			{
				"G",
				"Golf"
			},
			{
				"H",
				"Hotel"
			},
			{
				"I",
				"India"
			},
			{
				"J",
				"Juliet"
			},
			{
				"K",
				"Kilo"
			},
			{
				"L",
				"Lima"
			},
			{
				"M",
				"Mike"
			},
			{
				"N",
				"November"
			},
			{
				"O",
				"Oscar"
			},
			{
				"P",
				"Papa"
			},
			{
				"Q",
				"Quebec"
			},
			{
				"R",
				"Romeo"
			},
			{
				"S",
				"Sierra"
			},
			{
				"T",
				"Tango"
			},
			{
				"U",
				"Uniform"
			},
			{
				"V",
				"Victor"
			},
			{
				"W",
				"Whiskey"
			},
			{
				"X",
				"X ray"
			},
			{
				"Y",
				"Yankee"
			},
			{
				"Z",
				"Zulu"
			},
			{
				"0",
				"0"
			},
			{
				"1",
				"1"
			},
			{
				"2",
				"2"
			},
			{
				"3",
				"3"
			},
			{
				"4",
				"4"
			},
			{
				"5",
				"5"
			},
			{
				"6",
				"6"
			},
			{
				"7",
				"7"
			},
			{
				"8",
				"8"
			},
			{
				"9",
				"9"
			}
		};
	}
}
