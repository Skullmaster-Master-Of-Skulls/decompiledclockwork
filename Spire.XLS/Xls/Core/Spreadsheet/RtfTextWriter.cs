using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200061D RID: 1565
	public class RtfTextWriter : TextWriter
	{
		// Token: 0x06005EDA RID: 24282 RVA: 0x003B3FD8 File Offset: 0x003B2FD8
		public RtfTextWriter() : this(new StringWriter(), true)
		{
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x003B3FF4 File Offset: 0x003B2FF4
		public RtfTextWriter(bool enableFormatting) : this(new StringWriter(), enableFormatting)
		{
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x003B4010 File Offset: 0x003B3010
		public RtfTextWriter(TextWriter underlyingWriter) : this(underlyingWriter, true)
		{
		}

		// Token: 0x06005EDD RID: 24285 RVA: 0x003B4028 File Offset: 0x003B3028
		public RtfTextWriter(TextWriter underlyingWriter, bool enableFormatting)
		{
			this.ᜊ = underlyingWriter;
			this.ᜉ = enableFormatting;
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x003B406C File Offset: 0x003B306C
		protected virtual void OutputTabs()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (!this.ᜋ)
				{
					return;
				}
				break;
			}
			this.ᜋ = false;
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x003B40BC File Offset: 0x003B30BC
		protected string GetImageRTF(string rtf)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int num = rtf.IndexOf(RecordTableEnumerator.b("㵅ᑇ㩉╋ⵍ⑏", a_));
			int num2 = rtf.IndexOf(RecordTableEnumerator.b("㭅", a_), num);
			return rtf.Substring(num, num2 - num + 1);
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x003B4138 File Offset: 0x003B3138
		private void ᜀ(Font A_0)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (!this.ᜇ.ContainsKey(A_0))
					{
						num = 4;
						continue;
					}
					int num2 = this.ᜇ[A_0];
					this.Escape = false;
					LOGFONT logfont = new LOGFONT();
					A_0.ToLogFont(logfont);
					this.Write(string.Format(RecordTableEnumerator.b("䴵䌷昹娻䔽瀿㽁ᡃ⁅♇⍉⁋ቍ㙏ㅑ㱓㝕⩗⥙㥛⩝᭟卡ᥣ䙥፧塩ᅫ啭൯ཱ", a_), num2, logfont.lfCharSet, A_0.Name));
					this.Escape = true;
					num = 1;
					continue;
				}
				case 1:
					goto IL_FA;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_81;
				case 5:
					goto IL_4D;
				case 6:
					if (this.ᜉ)
					{
						num = 3;
						continue;
					}
					return;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					if (true)
					{
					}
					num = 6;
				}
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("倵圷吹䠻", a_));
			IL_81:
			throw new ApplicationException(RecordTableEnumerator.b("电圷嘹倻嬽⌿㙁ⵃ⥅♇橉⡋⅍㕏⅑瑓㡕㝗⹙籛㵝ཟౡၣݥŧѩ䱫࡭Ὧᱱs", a_));
			IL_FA:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4D;
			default:
				if (false)
				{
				}
				break;
			}
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x003B4288 File Offset: 0x003B3288
		private void ᜀ(int A_0, int A_1)
		{
			int a_ = 7;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				IL_39:
				switch (num)
				{
				case 1:
					goto IL_A7;
				case 2:
					this.Escape = false;
					this.ᜊ.Write(string.Format(RecordTableEnumerator.b("愼夾㩀獂㡄ᭆ⽈㡊㙌繎ⱐ", a_), A_0, A_1 * 2));
					this.Escape = true;
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_4B;
			}
			IL_A7:
			return;
			IL_4B:
			if (this.ᜉ)
			{
				num = 2;
				goto IL_39;
			}
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x003B4340 File Offset: 0x003B3340
		private void ᜀ(Color A_0)
		{
			int a_ = 5;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				IL_39:
				switch (num)
				{
				case 0:
					goto IL_B8;
				case 1:
					if (true)
					{
					}
					this.Escape = false;
					this.Write(string.Format(RecordTableEnumerator.b("机似娾╀㡂畄㩆ᕈⱊ㽌⩎㑐㵒⹔晖⑘ݚ㽜㍞ᑠ٢Ṥ啦ᑨ偪", a_), A_0.R, A_0.G, A_0.B));
					this.Escape = true;
					num = 0;
					continue;
				}
				goto IL_4B;
			}
			IL_B8:
			return;
			IL_4B:
			if (this.ᜉ)
			{
				num = 1;
				goto IL_39;
			}
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x003B4408 File Offset: 0x003B3408
		private void ᜀ(char A_0)
		{
			int a_ = 9;
			int num = 4;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_33;
				default:
					goto IL_33;
				}
				IL_5F:
				if (this.ᜌ)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				goto IL_160;
				IL_33:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0 == '}')
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
				case 1:
					if (A_0 == '\\')
					{
						num = 3;
						continue;
					}
					goto IL_D2;
				case 2:
					goto IL_15B;
				case 3:
					goto IL_97;
				case 5:
					goto IL_120;
				case 6:
					if (A_0 == '{')
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 7:
					num = 6;
					continue;
				}
				goto IL_5F;
			}
			IL_97:
			this.ᜊ.Write('\\');
			this.ᜊ.Write('\\');
			return;
			IL_D2:
			this.ᜊ.Write(RecordTableEnumerator.b("挾㑀", a_) + (int)A_0 + RecordTableEnumerator.b("ᔾ", a_));
			return;
			IL_120:
			this.ᜊ.Write('\\');
			this.ᜊ.Write('{');
			return;
			IL_15B:
			this.ᜊ.Write('\\');
			this.ᜊ.Write('}');
			return;
			IL_160:
			this.ᜊ.Write(A_0);
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x003B4584 File Offset: 0x003B3584
		private void ᜂ(string A_0)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C9;
				case 1:
					if (A_0.Length == 0)
					{
						num = 3;
						continue;
					}
					num = 8;
					continue;
				case 2:
				{
					int num2;
					int length;
					if (num2 >= length)
					{
						num = 5;
						continue;
					}
					this.Write(A_0[num2]);
					num2++;
					num = 0;
					continue;
				}
				case 3:
					goto IL_C7;
				case 4:
				{
					int num2 = 0;
					int length = A_0.Length;
					goto IL_9A;
				}
				case 5:
					return;
				case 6:
					goto IL_C9;
				case 8:
					if (this.ᜌ)
					{
						num = 4;
						continue;
					}
					goto IL_105;
				case 9:
					if (true)
					{
					}
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9A:
					num = 6;
					continue;
				default:
					if (false)
					{
					}
					if (A_0 != null)
					{
						num = 9;
						continue;
					}
					return;
				}
				IL_C9:
				num = 2;
			}
			return;
			IL_C7:
			return;
			IL_105:
			this.ᜊ.Write(A_0);
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x003B46A4 File Offset: 0x003B36A4
		private void ᜁ(string A_0)
		{
			for (;;)
			{
				IL_00:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						if (true)
						{
						}
						num = 3;
						continue;
					case 2:
						goto IL_74;
					case 3:
						if (A_0.Length == 0)
						{
							num = 2;
							continue;
						}
						goto IL_76;
					}
					if (A_0 == null)
					{
						return;
					}
					num = 1;
				}
			}
			return;
			IL_74:
			return;
			IL_76:
			this.ᜊ.Write(A_0);
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x003B4734 File Offset: 0x003B3734
		private void ᜀ(string A_0, string A_1, string A_2)
		{
			int num = 9;
			for (;;)
			{
				int num2;
				int length;
				switch (num)
				{
				case 0:
					if (A_0[num2 + 1] == 'G')
					{
						num = 14;
						continue;
					}
					goto IL_16F;
				case 1:
					num2 = 0;
					length = A_0.Length;
					num = 10;
					continue;
				case 2:
					num = 13;
					continue;
				case 3:
					if (A_1 != null)
					{
						num = 5;
						continue;
					}
					goto IL_16F;
				case 4:
					num = 0;
					continue;
				case 5:
					this.ᜁ(this.GetImageRTF(A_1));
					num2 += 2;
					num = 11;
					continue;
				case 6:
					return;
				case 7:
					if (A_0[num2] == '&')
					{
						num = 4;
						continue;
					}
					goto IL_16F;
				case 8:
					return;
				case 10:
					goto IL_B2;
				case 11:
					goto IL_111;
				case 12:
					if (num2 < length)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_111;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 13:
					if (A_0.Length == 0)
					{
						num = 16;
						continue;
					}
					num = 15;
					continue;
				case 14:
					num = 3;
					continue;
				case 15:
					if (true)
					{
					}
					if (this.ᜌ)
					{
						num = 1;
						continue;
					}
					goto IL_1D1;
				case 16:
					goto IL_16A;
				case 17:
					goto IL_B2;
				}
				if (A_0 != null)
				{
					num = 2;
					continue;
				}
				return;
				IL_B2:
				num = 12;
				continue;
				IL_111:
				if (num2 == length)
				{
					num = 6;
					continue;
				}
				IL_16F:
				this.Write(A_0[num2]);
				num2++;
				num = 17;
			}
			return;
			IL_16A:
			return;
			IL_1D1:
			this.ᜊ.Write(A_0);
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x003B4920 File Offset: 0x003B3920
		private void ᜀ()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (this.ᜌ)
				{
					if (true)
					{
					}
					this.ᜊ.Write(RtfTextWriter.\u170D);
					return;
				}
				break;
			}
			this.ᜊ.WriteLine();
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x003B4984 File Offset: 0x003B3984
		private void ᜀ(string A_0)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (this.ᜌ)
				{
					this.Write(A_0);
					this.ᜊ.Write(RtfTextWriter.\u170D);
					return;
				}
				break;
			}
			this.ᜊ.WriteLine(A_0);
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x003B49F0 File Offset: 0x003B39F0
		public override string ToString()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜊ.ToString();
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x003B4A38 File Offset: 0x003B3A38
		public override void Write(bool value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x003B4A88 File Offset: 0x003B3A88
		public override void Write(char value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜀ(value);
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x003B4AD0 File Offset: 0x003B3AD0
		public override void Write(char[] buffer)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(buffer);
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x003B4B20 File Offset: 0x003B3B20
		public override void Write(double value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x003B4B70 File Offset: 0x003B3B70
		public override void Write(int value)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x003B4BC0 File Offset: 0x003B3BC0
		public override void Write(long value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x003B4C10 File Offset: 0x003B3C10
		public override void Write(object value)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x003B4C60 File Offset: 0x003B3C60
		public override void Write(float value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x003B4CB0 File Offset: 0x003B3CB0
		public override void Write(string s)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜂ(s);
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x003B4CF8 File Offset: 0x003B3CF8
		internal void ᜁ(string A_0, string A_1, string A_2)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜀ(A_0, A_1, A_2);
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x003B4D44 File Offset: 0x003B3D44
		[CLSCompliant(false)]
		public override void Write(uint value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(value);
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x003B4D94 File Offset: 0x003B3D94
		public override void Write(string format, object arg0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(format, arg0);
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x003B4DE4 File Offset: 0x003B3DE4
		public override void Write(string format, params object[] arg)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(format, arg);
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x003B4E34 File Offset: 0x003B3E34
		public override void Write(string format, object arg0, object arg1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(format, arg0, arg1);
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x003B4E84 File Offset: 0x003B3E84
		public override void Write(char[] buffer, int index, int count)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.Write(buffer, index, count);
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x003B4ED4 File Offset: 0x003B3ED4
		public override void WriteLine()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜀ();
			this.ᜋ = true;
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x003B4F24 File Offset: 0x003B3F24
		public override void WriteLine(bool value)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x003B4F78 File Offset: 0x003B3F78
		public override void WriteLine(char value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x003B4FCC File Offset: 0x003B3FCC
		public override void WriteLine(char[] buffer)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(buffer);
			this.ᜋ = true;
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x003B5020 File Offset: 0x003B4020
		public override void WriteLine(double value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x003B5074 File Offset: 0x003B4074
		public override void WriteLine(int value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x003B50C8 File Offset: 0x003B40C8
		public override void WriteLine(long value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x003B511C File Offset: 0x003B411C
		public override void WriteLine(object value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x003B5170 File Offset: 0x003B4170
		public override void WriteLine(float value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x003B51C4 File Offset: 0x003B41C4
		public override void WriteLine(string s)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜀ(s);
			this.ᜋ = true;
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x003B5214 File Offset: 0x003B4214
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(value);
			this.ᜋ = true;
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x003B5268 File Offset: 0x003B4268
		public override void WriteLine(string format, params object[] arg)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(format, arg);
			this.ᜋ = true;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x003B52C0 File Offset: 0x003B42C0
		public override void WriteLine(string format, object arg0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(format, arg0);
			this.ᜋ = true;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x003B5318 File Offset: 0x003B4318
		public override void WriteLine(string format, object arg0, object arg1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(format, arg0, arg1);
			this.ᜋ = true;
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x003B5370 File Offset: 0x003B4370
		public override void WriteLine(char[] buffer, int index, int count)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.OutputTabs();
			this.ᜊ.WriteLine(buffer, index, count);
			this.ᜋ = true;
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x003B53C8 File Offset: 0x003B43C8
		public int AddFont(Font font)
		{
			int a_ = 11;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_29;
			default:
				goto IL_29;
			}
			int num;
			for (;;)
			{
				IL_39:
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 1:
					goto IL_8F;
				case 3:
					if (this.ᜇ.ContainsKey(font))
					{
						num = 1;
						continue;
					}
					goto IL_A5;
				}
				if (font == null)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_5A:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ⭄㍆", a_));
			IL_8F:
			return this.ᜇ[font];
			IL_A5:
			int num2 = this.ᜇ.Count + 1;
			this.ᜇ.Add(font, num2);
			return num2;
			IL_29:
			if (false)
			{
			}
			num = 2;
			goto IL_39;
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x003B5498 File Offset: 0x003B4498
		public int AddColor(Color color)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_92;
				case 1:
					this.ᜈ.Add(color, this.ᜈ.Count + 1);
					this.ᜆ.Add(color);
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				}
				if (false)
				{
				}
				if (this.ᜈ.ContainsKey(color))
				{
					break;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_92:
			IL_94:
			return this.ᜈ[color];
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x003B5548 File Offset: 0x003B4548
		public void WriteFontTable()
		{
			int num = 2;
			for (;;)
			{
				Dictionary<Font, int>.KeyCollection.Enumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								Font a_ = enumerator.Current;
								this.ᜀ(a_);
								num = 2;
								continue;
							}
							case 3:
								goto IL_C9;
							case 4:
								num = 3;
								continue;
							}
							IL_A6:
							num = 0;
							continue;
							goto IL_A6;
						}
						IL_C9:
						goto IL_102;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_D9;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_54;
					}
					break;
				}
				goto IL_1C;
				IL_2C:
				num = 1;
				continue;
				IL_1C:
				if (this.ᜇ.Count == 0)
				{
					goto IL_2C;
				}
				IL_D9:
				this.WriteTag(RtfTags.FontTableBegin);
				enumerator = this.ᜇ.Keys.GetEnumerator();
				num = 0;
			}
			IL_54:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
			IL_102:
			this.WriteTag(RtfTags.FontTableEnd);
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x003B5670 File Offset: 0x003B4670
		public void WriteColorTable()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CC;
				case 1:
					return;
				case 2:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					Color a_ = this.ᜆ[num2];
					this.ᜀ(a_);
					num2++;
					if (true)
					{
					}
					num = 5;
					continue;
				}
				case 4:
					goto IL_A8;
				case 5:
					goto IL_A8;
				}
				if (this.ᜈ.Count == 0)
				{
					num = 1;
					continue;
				}
				for (;;)
				{
					this.WriteTag(RtfTags.ColorTableStart);
					int num2 = 0;
					int count = this.ᜆ.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_94;
					}
				}
				IL_94:
				if (false)
				{
				}
				num = 4;
				continue;
				IL_A8:
				num = 2;
			}
			return;
			IL_CC:
			this.WriteTag(RtfTags.ColorTableEnd);
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x003B5754 File Offset: 0x003B4754
		public void WriteText(Font font, string strText)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.WriteText(font, spr\u1D39.ᜂ, spr\u1D39.ᜂ, strText);
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x003B57A4 File Offset: 0x003B47A4
		public void WriteText(Font font, Color foreColor, string strText)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.WriteText(font, foreColor, spr\u1D39.ᜂ, strText);
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x003B57F0 File Offset: 0x003B47F0
		public void WriteText(Font font, Color foreColor, Color backColor, string strText)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					bool flag;
					int num3;
					int length;
					switch (num)
					{
					case 0:
						if (strText.Length == 0)
						{
							num = 10;
							continue;
						}
						this.WriteTag(RtfTags.GroupStart);
						this.WriteFont(font);
						num = 18;
						continue;
					case 1:
						goto IL_1BA;
					case 2:
						if (backColor != spr\u1D39.ᜂ)
						{
							num = 21;
							continue;
						}
						goto IL_B0;
					case 3:
						goto IL_16B;
					case 4:
						num2 = strText.Length;
						flag = false;
						num = 13;
						continue;
					case 6:
						goto IL_187;
					case 7:
						num = 0;
						continue;
					case 8:
						goto IL_F6;
					case 9:
						if (num2 == -1)
						{
							num = 4;
							continue;
						}
						goto IL_1FF;
					case 10:
						goto IL_F1;
					case 11:
						if (strText != null)
						{
							num = 7;
							continue;
						}
						return;
					case 12:
						goto IL_16B;
					case 13:
						goto IL_1FF;
					case 14:
						this.WriteForeColorAttribute(foreColor);
						num = 19;
						continue;
					case 15:
						goto IL_90;
					case 16:
						goto IL_B0;
					case 17:
						if (flag)
						{
							num = 8;
							continue;
						}
						goto IL_1BA;
					case 18:
						if (foreColor != spr\u1D39.ᜂ)
						{
							num = 14;
							continue;
						}
						goto IL_18C;
					case 19:
						goto IL_18C;
					case 20:
						if (num3 >= length)
						{
							num = 6;
							continue;
						}
						if (true)
						{
						}
						num2 = strText.IndexOf(this.NewLine, num3);
						num = 9;
						continue;
					case 21:
						this.WriteBackColorAttribute(backColor);
						num = 16;
						continue;
					}
					if (font == null)
					{
						num = 15;
						continue;
					}
					num = 11;
					continue;
					IL_B0:
					num3 = 0;
					length = strText.Length;
					flag = true;
					num = 12;
					continue;
					IL_F6:
					this.WriteTag(RtfTags.EndLine);
					num = 1;
					continue;
					IL_1BA:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F6;
					default:
						if (false)
						{
						}
						num3 = num2 + this.NewLine.Length;
						num = 3;
						continue;
					}
					IL_16B:
					num = 20;
					continue;
					IL_18C:
					num = 2;
					continue;
					IL_1FF:
					string value = strText.Substring(num3, num2 - num3);
					this.Write(value);
					num = 17;
				}
				IL_90:
				throw new ArgumentNullException(RecordTableEnumerator.b("⑁⭃⡅㱇", a_));
				IL_F1:
				return;
				IL_187:
				this.WriteTag(RtfTags.GroupEnd);
				return;
			}
			}
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x003B5AB8 File Offset: 0x003B4AB8
		public void WriteText(IFont font, string strText)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
					{
						num2 = strText.Length;
						bool flag = false;
						num = 14;
						continue;
					}
					case 1:
						goto IL_165;
					case 2:
						if (strText != null)
						{
							num = 5;
							continue;
						}
						return;
					case 3:
						if (num2 == -1)
						{
							num = 0;
							continue;
						}
						goto IL_7D;
					case 4:
						goto IL_11F;
					case 5:
						num = 7;
						continue;
					case 6:
					{
						int length;
						if (num3 >= length)
						{
							num = 8;
							continue;
						}
						num2 = strText.IndexOf(this.NewLine, num3);
						num = 3;
						continue;
					}
					case 7:
					{
						if (strText.Length == 0)
						{
							num = 15;
							continue;
						}
						this.WriteTag(RtfTags.GroupStart);
						this.WriteFont(font);
						num3 = 0;
						int length = strText.Length;
						bool flag = true;
						if (true)
						{
						}
						num = 1;
						continue;
					}
					case 8:
						goto IL_18E;
					case 10:
					{
						bool flag;
						if (flag)
						{
							num = 13;
							continue;
						}
						goto IL_11F;
					}
					case 11:
						goto IL_165;
					case 12:
						goto IL_78;
					case 13:
						this.WriteTag(RtfTags.EndLine);
						num = 4;
						continue;
					case 14:
						goto IL_7D;
					case 15:
						return;
					}
					if (font == null)
					{
						num = 12;
						continue;
					}
					num = 2;
					continue;
					IL_7D:
					string value = strText.Substring(num3, num2 - num3);
					this.Write(value);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					IL_11F:
					num3 = num2 + this.NewLine.Length;
					num = 11;
					continue;
					IL_165:
					num = 6;
				}
				IL_78:
				throw new ArgumentNullException(RecordTableEnumerator.b("崺刼儾㕀", a_));
				IL_18E:
				this.WriteTag(RtfTags.GroupEnd);
				return;
			}
			}
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x003B5CCC File Offset: 0x003B4CCC
		internal void ᜀ(IFont A_0, string A_1, string A_2, string A_3)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 1:
						goto IL_168;
					case 2:
						num = 3;
						continue;
					case 3:
					{
						if (A_1.Length == 0)
						{
							num = 14;
							continue;
						}
						this.WriteTag(RtfTags.GroupStart);
						this.WriteFont(A_0);
						num2 = 0;
						int length = A_1.Length;
						bool flag = true;
						num = 9;
						continue;
					}
					case 4:
						goto IL_78;
					case 5:
					{
						bool flag;
						if (flag)
						{
							num = 13;
							continue;
						}
						goto IL_122;
					}
					case 6:
						if (num3 == -1)
						{
							num = 10;
							continue;
						}
						goto IL_7D;
					case 7:
						goto IL_7D;
					case 8:
						goto IL_191;
					case 9:
						goto IL_168;
					case 10:
					{
						num3 = A_1.Length;
						bool flag = false;
						num = 7;
						continue;
					}
					case 11:
						goto IL_122;
					case 12:
						if (A_1 != null)
						{
							num = 2;
							continue;
						}
						return;
					case 13:
						this.WriteTag(RtfTags.EndLine);
						num = 11;
						continue;
					case 14:
						goto IL_1DB;
					case 15:
					{
						int length;
						if (num2 >= length)
						{
							num = 8;
							continue;
						}
						num3 = A_1.IndexOf(this.NewLine, num2);
						num = 6;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 12;
					continue;
					IL_7D:
					string a_2 = A_1.Substring(num2, num3 - num2);
					this.ᜁ(a_2, A_2, A_3);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_122:
					num2 = num3 + this.NewLine.Length;
					num = 1;
					continue;
					IL_168:
					num = 15;
				}
				IL_78:
				throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ⱁぃ", a_));
				IL_191:
				this.WriteTag(RtfTags.GroupEnd);
				return;
				IL_1DB:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x003B5EE4 File Offset: 0x003B4EE4
		public void WriteFontAttribute(Font font)
		{
			int a_ = 2;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (!this.ᜇ.ContainsKey(font))
					{
						num = 2;
						continue;
					}
					goto IL_AF;
				case 2:
					goto IL_99;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						goto IL_4A;
					}
					break;
				}
				if (font == null)
				{
					num = 3;
					continue;
				}
				IL_6E:
				num = 1;
			}
			IL_4A:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹刻䨽", a_));
			IL_99:
			throw new ArgumentException(RecordTableEnumerator.b("洷吹圻倽⼿㕁⩃晅⹇╉≋㩍", a_));
			IL_AF:
			int a_2 = this.ᜇ[font];
			int a_3 = (int)font.Size;
			this.ᜀ(a_2, a_3);
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x003B5FC0 File Offset: 0x003B4FC0
		public void WriteFont(Font font)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.WriteFontAttribute(font);
				this.WriteFontItalicBoldStriked(font);
				int num = 1;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						return;
					case 1:
						while (font.Underline)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 2;
								goto IL_02;
							}
						}
						return;
					case 2:
						this.WriteUnderlineAttribute();
						num = 0;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x003B6048 File Offset: 0x003B5048
		public void WriteFont(IFont font)
		{
			int a_ = 14;
			int num = 6;
			XlsFont font2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (font is FontWrapper)
					{
						num = 4;
						continue;
					}
					goto IL_B0;
				case 1:
					goto IL_67;
				case 2:
					goto IL_7B;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_73;
					default:
						goto IL_96;
					}
					break;
				case 4:
					font2 = ((FontWrapper)font).Wrapped;
					num = 1;
					continue;
				case 5:
					if (font is XlsFont)
					{
						num = 7;
						continue;
					}
					num = 0;
					continue;
				case 7:
					font2 = (XlsFont)font;
					goto IL_73;
				}
				if (font == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_73:
				num = 2;
			}
			IL_67:
			IL_7B:
			goto IL_107;
			IL_96:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅♇㹉", a_));
			IL_B0:
			throw new InvalidCastException(RecordTableEnumerator.b("ൃ⡅㹇⭉⁋❍㑏牑⁓⽕⡗㽙牛", a_));
			IL_107:
			if (true)
			{
			}
			Font font3 = font.GenerateNativeFont();
			this.WriteFontAttribute(font3);
			this.WriteFontItalicBoldStriked(font3);
			this.WriteUnderline(font2);
			this.WriteSubSuperScript(font2);
			this.WriteForeColorAttribute(font.Color);
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x003B6194 File Offset: 0x003B5194
		public void WriteSubSuperScript(XlsFont font)
		{
			int a_ = 1;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B7;
				case 1:
					this.WriteTag(RtfTags.SuperScript);
					num = 0;
					continue;
				case 2:
					goto IL_D7;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					}
					if (false)
					{
					}
					break;
				case 4:
					goto IL_66;
				case 5:
					if (font.IsSubscript)
					{
						num = 2;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (font.IsSuperscript)
					{
						num = 1;
						continue;
					}
					return;
				}
				if (font == null)
				{
					num = 4;
				}
				else
				{
					num = 5;
				}
			}
			IL_66:
			IL_8B:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("儶嘸唺䤼", a_));
			IL_B7:
			return;
			IL_D7:
			this.WriteTag(RtfTags.SubScript);
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x003B627C File Offset: 0x003B527C
		public void WriteFontItalicBoldStriked(Font font)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (font.Italic)
					{
						num = 6;
						continue;
					}
					goto IL_82;
				case 2:
					if (font.Bold)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_EF;
				case 3:
					if (font.Strikeout)
					{
						num = 4;
						continue;
					}
					return;
				case 4:
					this.WriteStrikeThrough(StrikeThroughStyle.SingleOn);
					num = 9;
					continue;
				case 5:
					goto IL_EF;
				case 6:
					this.WriteTag(RtfTags.ItalicOn);
					num = 7;
					continue;
				case 7:
					goto IL_121;
				case 8:
					goto IL_50;
				case 9:
					return;
				case 10:
					this.WriteTag(RtfTags.BoldOn);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_121;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				if (font == null)
				{
					num = 8;
					continue;
				}
				num = 0;
				continue;
				IL_82:
				num = 2;
				continue;
				IL_121:
				goto IL_82;
				IL_EF:
				num = 3;
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅❇⑉㡋", a_));
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x003B63BC File Offset: 0x003B53BC
		public void WriteUnderline(XlsFont font)
		{
			int a_ = 3;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					FontUnderlineType underline;
					switch (underline)
					{
					case FontUnderlineType.SingleAccounting:
						goto IL_E0;
					case FontUnderlineType.DoubleAccounting:
						goto IL_A4;
					default:
						num = 5;
						continue;
					}
					break;
				}
				case 2:
				{
					FontUnderlineType underline;
					switch (underline)
					{
					case FontUnderlineType.None:
						return;
					case FontUnderlineType.Single:
						goto IL_E0;
					case FontUnderlineType.Double:
						goto IL_A4;
					default:
						num = 3;
						continue;
					}
					break;
				}
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_44;
				case 5:
					goto IL_68;
				}
				if (font == null)
				{
					if (true)
					{
					}
					num = 4;
				}
				else
				{
					FontUnderlineType underline = font.Underline;
					num = 2;
				}
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸吺匼䬾", a_));
			IL_68:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E0:
				this.WriteUnderlineAttribute(UnderlineStyle.Continuous);
				return;
			}
			if (false)
			{
			}
			return;
			IL_A4:
			this.WriteUnderlineAttribute(UnderlineStyle.Double);
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x003B64B0 File Offset: 0x003B54B0
		public void WriteUnderlineAttribute()
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.WriteUnderlineAttribute(UnderlineStyle.Continuous);
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					if (!this.ᜉ)
					{
						return;
					}
					num = 2;
					break;
				}
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x003B652C File Offset: 0x003B552C
		public void WriteUnderlineAttribute(UnderlineStyle style)
		{
			int a_ = 8;
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (style < UnderlineStyle.Continuous)
						{
							goto IL_55;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						if (style >= (UnderlineStyle)RtfTextWriter.ᜃ.Length)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_9A;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_98;
					}
					break;
				}
			}
			IL_55:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴽㐿㭁⡃⍅", a_));
			IL_98:
			goto IL_55;
			IL_9A:
			this.Escape = false;
			this.Write(RtfTextWriter.ᜃ[(int)style]);
			this.Escape = true;
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x003B65F0 File Offset: 0x003B55F0
		public void WriteStrikeThrough(StrikeThroughStyle style)
		{
			int a_ = 13;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (style >= (StrikeThroughStyle)RtfTextWriter.ᜄ.Length)
						{
							num = 3;
							continue;
						}
						goto IL_9A;
					case 1:
						IL_37:
						num = 0;
						continue;
					case 2:
						if (style >= StrikeThroughStyle.SingleOn)
						{
							num = 1;
							continue;
						}
						goto IL_39;
					case 3:
						goto IL_39;
					}
					break;
					IL_39:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						goto IL_4F;
					}
				}
			}
			IL_4F:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("あㅄ㹆╈⹊", a_));
			IL_9A:
			this.Escape = false;
			this.Write(RtfTextWriter.ᜄ[(int)style]);
			this.Escape = true;
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x003B66B4 File Offset: 0x003B56B4
		public void WriteBackColorAttribute(Color color)
		{
			int a_ = 19;
			if (true)
			{
			}
			if (this.ᜈ.ContainsKey(color))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num = this.ᜈ[color];
					this.WriteTag(RtfTags.BackColor, new object[]
					{
						num
					});
					return;
				}
				}
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩈⑊⅌⁎⍐", a_), RecordTableEnumerator.b("᱈╊♌ⅎ㹐⑒㭔睖㩘㑚ㅜぞ፠", a_));
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x003B6750 File Offset: 0x003B5750
		public void WriteForeColorAttribute(Color color)
		{
			int a_ = 4;
			if (this.ᜈ.ContainsKey(color))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num = this.ᜈ[color];
					this.WriteTag(RtfTags.ForeColor, new object[]
					{
						num
					});
					return;
				}
				}
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夹医刽⼿ぁ", a_), RecordTableEnumerator.b("漹刻唽⸿ⵁ㍃⡅桇⥉⍋≍㽏⁑", a_));
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x003B67EC File Offset: 0x003B57EC
		public void WriteLineNoTabs(string s)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜊ.WriteLine(s);
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x003B6834 File Offset: 0x003B5834
		public void WriteTag(RtfTags tag)
		{
			int a_ = 15;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_76:
				num = 0;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (tag >= RtfTags.FontTableBegin)
					{
						num = 6;
						continue;
					}
					goto IL_78;
				case 1:
					if (tag >= (RtfTags)RtfTextWriter.ᜅ.Length)
					{
						num = 2;
						continue;
					}
					this.Escape = false;
					this.ᜊ.Write(RtfTextWriter.ᜅ[(int)tag]);
					this.Escape = true;
					num = 4;
					continue;
				case 2:
					goto IL_A6;
				case 4:
					return;
				case 5:
					goto IL_76;
				case 6:
					num = 1;
					continue;
				}
				if (!this.ᜉ)
				{
					return;
				}
				num = 5;
			}
			IL_78:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅄ♆⹈", a_));
			IL_A6:
			goto IL_78;
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x003B6934 File Offset: 0x003B5934
		public void WriteTag(RtfTags tag, params object[] arrParams)
		{
			int a_ = 16;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_64:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (tag >= RtfTags.FontTableBegin)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					goto IL_70;
				case 2:
					goto IL_9E;
				case 3:
					return;
				case 4:
					goto IL_64;
				case 5:
					if (tag >= (RtfTags)RtfTextWriter.ᜅ.Length)
					{
						num = 2;
						continue;
					}
					this.Escape = false;
					this.ᜊ.Write(string.Format(RtfTextWriter.ᜅ[(int)tag], arrParams));
					this.Escape = true;
					num = 3;
					continue;
				case 6:
					num = 5;
					continue;
				}
				if (!this.ᜉ)
				{
					return;
				}
				num = 4;
			}
			IL_70:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉅⥇ⵉ", a_));
			IL_9E:
			goto IL_70;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x003B6A38 File Offset: 0x003B5A38
		internal void ᜃ(string A_0)
		{
			int a_ = 13;
			string arg;
			for (;;)
			{
				arg = RecordTableEnumerator.b("㉂⥄", a_);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 != null)
						{
							num = 5;
							continue;
						}
						goto IL_198;
					case 1:
						goto IL_BD;
					case 2:
						num = 7;
						continue;
					case 3:
						if (!(A_0 == RecordTableEnumerator.b("ག⁄ⅆ㵈", a_)))
						{
							num = 2;
							continue;
						}
						arg = RecordTableEnumerator.b("㉂⥄", a_);
						num = 11;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							goto IL_AA;
						}
						break;
					case 5:
						num = 6;
						continue;
					case 6:
						if (!(A_0 == RecordTableEnumerator.b("B⁄⥆㵈⹊㽌", a_)))
						{
							num = 1;
							continue;
						}
						arg = RecordTableEnumerator.b("㉂♄", a_);
						num = 9;
						continue;
					case 7:
						if (!(A_0 == RecordTableEnumerator.b("ᅂⱄ⁆ⅈ㽊", a_)))
						{
							num = 10;
							continue;
						}
						arg = RecordTableEnumerator.b("㉂㝄", a_);
						num = 8;
						continue;
					case 8:
						goto IL_10F;
					case 9:
						goto IL_84;
					case 10:
						num = 4;
						continue;
					case 11:
						goto IL_162;
					}
					break;
					IL_BD:
					num = 3;
				}
			}
			IL_84:
			goto IL_198;
			IL_AA:
			if (true)
			{
			}
			if (false)
			{
			}
			IL_10F:
			IL_162:
			IL_198:
			this.Escape = false;
			this.ᜊ.Write(string.Format(RecordTableEnumerator.b("ὂ㕄♆㭈⽊ᅌ㑎慐⹒", a_), arg));
			this.Escape = true;
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x003B6C0C File Offset: 0x003B5C0C
		// (set) Token: 0x06005F21 RID: 24353 RVA: 0x003B6C50 File Offset: 0x003B5C50
		public bool Escape
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜌ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜌ = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005F22 RID: 24354 RVA: 0x003B6C94 File Offset: 0x003B5C94
		public override Encoding Encoding
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜊ.Encoding;
			}
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x003B6CDC File Offset: 0x003B5CDC
		// Note: this type is marked as 'beforefieldinit'.
		static RtfTextWriter()
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			RtfTextWriter.ᜃ = new string[]
			{
				RecordTableEnumerator.b("᥄㉆╈", a_),
				RecordTableEnumerator.b("᥄㉆╈筊", a_),
				RecordTableEnumerator.b("᥄㉆╈⽊", a_),
				RecordTableEnumerator.b("᥄㉆╈⽊ⱌ㱎㥐", a_),
				RecordTableEnumerator.b("᥄㉆╈⽊ⱌ㱎㥐㝒", a_),
				RecordTableEnumerator.b("᥄㉆╈⽊ⱌ㱎㥐㝒ㅔ", a_),
				RecordTableEnumerator.b("᥄㉆╈⽊⽌", a_),
				RecordTableEnumerator.b("᥄㉆╈⍊㩌⹎❐㙒", a_),
				RecordTableEnumerator.b("᥄㉆╈❊⥌⹎≐㭒", a_),
				RecordTableEnumerator.b("᥄㉆╈╊≌ⅎ㑐", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌⭎", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌⭎ぐ⁒㵔", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌⭎ぐ⁒㵔㍖", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌⭎ぐ⁒㵔㍖㵘", a_),
				RecordTableEnumerator.b("᥄㉆╈㽊╌⍎㕐㉒♔㽖", a_),
				RecordTableEnumerator.b("᥄㉆╈㹊⅌⭎㍐⑒㑔⅖㱘", a_),
				RecordTableEnumerator.b("᥄㉆╈㱊", a_),
				RecordTableEnumerator.b("᥄㉆╈㱊ⱌ㥎㑐", a_)
			};
			RtfTextWriter.ᜄ = new string[]
			{
				RecordTableEnumerator.b("᥄㑆㵈㥊⑌⑎㑐扒", a_),
				RecordTableEnumerator.b("᥄㑆㵈㥊⑌⑎㑐捒", a_),
				RecordTableEnumerator.b("᥄㑆㵈㥊⑌⑎㑐㝒摔", a_),
				RecordTableEnumerator.b("᥄㑆㵈㥊⑌⑎㑐㝒敔", a_)
			};
			RtfTextWriter.ᜅ = new string[]
			{
				RecordTableEnumerator.b("㹄ᭆ⽈⑊⍌㭎═ㅒ㥔", a_),
				RecordTableEnumerator.b("㡄", a_),
				RecordTableEnumerator.b("㹄ᭆ⩈⑊⅌⁎⍐❒㝔㭖祘恚", a_),
				RecordTableEnumerator.b("㡄", a_),
				RecordTableEnumerator.b("᥄╆", a_),
				RecordTableEnumerator.b("᥄╆祈", a_),
				RecordTableEnumerator.b("᥄⹆", a_),
				RecordTableEnumerator.b("᥄⹆祈", a_),
				RecordTableEnumerator.b("㹄ᭆ㭈㽊⭌繎൐㉒㭔⑖じݚ㱜ㅞበ੢٤ᝦ๨婪彬婮䍰⽲ᅴቶὸᵺ䵼⍾뺎ꆐꂒꚔ", a_),
				RecordTableEnumerator.b("㡄", a_),
				RecordTableEnumerator.b("㹄", a_),
				RecordTableEnumerator.b("㡄", a_),
				RecordTableEnumerator.b("᥄㝆⡈㥊", a_),
				RecordTableEnumerator.b("᥄⑆⽈お経㉎", a_),
				RecordTableEnumerator.b("᥄⑆⭈お経㉎", a_),
				RecordTableEnumerator.b("᥄㑆㱈⥊", a_),
				RecordTableEnumerator.b("᥄㑆㱈㭊⡌㵎", a_),
				RecordTableEnumerator.b("᥄⥆♈㡊㡌㽎㑐⅒♔≖㭘", a_)
			};
			RtfTextWriter.\u170D = RecordTableEnumerator.b("᥄⭆⁈╊⡌䉎子", a_).ToCharArray();
		}

		// Token: 0x04002D8B RID: 11659
		private const string ᜀ = "{{\\f{0}\\fnil\\fcharset{1} {2};}}";

		// Token: 0x04002D8C RID: 11660
		private const string ᜁ = "\\f{0}\\fs{1}";

		// Token: 0x04002D8D RID: 11661
		private const string ᜂ = "\\red{0}\\green{1}\\blue{2};";

		// Token: 0x04002D8E RID: 11662
		private static readonly string[] ᜃ;

		// Token: 0x04002D8F RID: 11663
		private static readonly string[] ᜄ;

		// Token: 0x04002D90 RID: 11664
		internal static readonly string[] ᜅ;

		// Token: 0x04002D91 RID: 11665
		private List<Color> ᜆ = new List<Color>();

		// Token: 0x04002D92 RID: 11666
		private bool[] \u2593\u00AC\u007F\u009E;

		// Token: 0x04002D93 RID: 11667
		private Dictionary<Font, int> ᜇ = new Dictionary<Font, int>();

		// Token: 0x04002D94 RID: 11668
		private Dictionary<Color, int> ᜈ = new Dictionary<Color, int>();

		// Token: 0x04002D95 RID: 11669
		private bool ᜉ;

		// Token: 0x04002D96 RID: 11670
		private TextWriter ᜊ;

		// Token: 0x04002D97 RID: 11671
		private bool ᜋ;

		// Token: 0x04002D98 RID: 11672
		private bool ᜌ;

		// Token: 0x04002D99 RID: 11673
		private static readonly char[] \u170D;
	}
}
