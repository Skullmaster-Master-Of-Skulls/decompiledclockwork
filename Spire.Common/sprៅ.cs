using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using Spire.License;
using Spire.Pdf;
using Spire.Xls;
using Spire.Xls.Converter;

// Token: 0x02000007 RID: 7
internal static class spr\u17C5
{
	// Token: 0x06000012 RID: 18 RVA: 0x00002358 File Offset: 0x00000558
	internal static void ᜀ(Workbook A_0, PdfDocument A_1)
	{
		int a_ = 18;
		for (;;)
		{
			switch (0)
			{
			default:
				if (A_0 != null)
				{
					goto IL_4D;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_38;
				}
				break;
			}
		}
		IL_38:
		if (false)
		{
		}
		return;
		try
		{
			IL_4D:
			for (;;)
			{
				License license = null;
				LicenseManager.IsValid(typeof(Workbook), A_0, out license);
				LicenseType licenseType = spr\u2067.ᜀ(license);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_FD;
					case 1:
						goto IL_109;
					case 2:
						if ((licenseType & LicenseType.Runtime) == LicenseType.Runtime)
						{
							num = 3;
							continue;
						}
						goto IL_FD;
					case 3:
						A_1.InternalLicense = new InternalLicense
						{
							License = (LicenseInfo)license,
							LicenseType = licenseType,
							ProductName = SheetFinishedEventHandler.b("髈믊꓌뷎듐﷒跔鯖諘", a_),
							AssemblyList = new string[]
							{
								SheetFinishedEventHandler.b("髈믊꓌뷎듐﷒跔鯖諘", a_)
							}
						};
						num = 0;
						continue;
					}
					break;
					IL_FD:
					num = 1;
				}
			}
			IL_109:
			return;
		}
		catch (Exception)
		{
			return;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002484 File Offset: 0x00000684
	internal static IEnumerable<ᜀ> ᜀ<ᜀ>(IEnumerable A_0)
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
		spr\u17C5<ᜀ>.ᜀ ᜀ = new spr\u17C5<ᜀ>.ᜀ(-2);
		ᜀ.ᜄ = A_0;
		return ᜀ;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000024D0 File Offset: 0x000006D0
	internal static IEnumerable<ᜀ> ᜀ<ᜀ>(object A_0)
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
		spr\u17C5<ᜀ>.ᜁ ᜁ = new spr\u17C5<ᜀ>.ᜁ(-2);
		ᜁ.ᜄ = A_0;
		return ᜁ;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000251C File Offset: 0x0000071C
	internal static Font ᜀ(string A_0, float A_1)
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
		sprᲽ sprᲽ = new sprᲽ();
		sprᲽ.ᜁ(A_0);
		sprᲽ.ᜀ(A_1);
		sprᲽ.ᜀ(FontStyle.Regular);
		return sprᲽ.ᜊ();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002578 File Offset: 0x00000778
	internal static bool ᜀ(ref string A_0)
	{
		while (string.IsNullOrEmpty(A_0))
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
				if (true)
				{
				}
				return true;
			}
		}
		A_0 = A_0.Trim();
		return A_0.Length == 0;
	}

	// Token: 0x02000008 RID: 8
	[CompilerGenerated]
	private sealed class ᜀ<ᜀ> : IEnumerable<ᜀ>, IEnumerator<ᜀ>
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000025D4 File Offset: 0x000007D4
		[DebuggerHidden]
		IEnumerator<ᜀ> IEnumerable<!0>.ᜁ()
		{
			int num = 5;
			spr\u17C5<ᜀ>.ᜀ ᜀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						if (false)
						{
						}
						if (this.ᜁ == -2)
						{
							num = 4;
							continue;
						}
						goto IL_4C;
					}
					break;
				case 1:
					goto IL_5B;
				case 2:
					goto IL_6E;
				case 3:
					if (true)
					{
					}
					num = 0;
					continue;
				case 4:
					this.ᜁ = 0;
					ᜀ = this;
					num = 2;
					continue;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.ᜂ)
				{
					num = 3;
					continue;
				}
				IL_4C:
				ᜀ = new spr\u17C5<ᜀ>.ᜀ(0);
				num = 1;
			}
			IL_5B:
			IL_6E:
			ᜀ.ᜃ = this.ᜄ;
			return ᜀ;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026A4 File Offset: 0x000008A4
		[DebuggerHidden]
		IEnumerator IEnumerable.ᜃ()
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
			return this.ᜁ();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026E8 File Offset: 0x000008E8
		bool IEnumerator.ᜇ()
		{
			bool result;
			try
			{
				for (;;)
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_128;
							case 1:
								goto IL_5B;
							case 2:
								if (!this.ᜆ.MoveNext())
								{
									num2 = 3;
									continue;
								}
								this.ᜅ = (ᜀ)((object)this.ᜆ.Current);
								this.ᜀ = this.ᜅ;
								this.ᜁ = 2;
								result = true;
								num2 = 8;
								continue;
							case 3:
								this.ᜀ();
								num2 = 5;
								continue;
							case 4:
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜆ = this.ᜃ.GetEnumerator();
									this.ᜁ = 1;
									num2 = 7;
									continue;
								case 1:
									goto IL_11B;
								case 2:
									this.ᜁ = 1;
									num2 = 1;
									continue;
								default:
									num2 = 9;
									continue;
								}
								break;
							case 5:
								goto IL_11B;
							case 6:
								goto IL_11B;
							case 7:
								goto IL_5B;
							case 8:
								goto IL_B0;
							case 9:
								num2 = 6;
								continue;
							}
							break;
							IL_5B:
							num2 = 2;
							continue;
							IL_11B:
							result = false;
							num2 = 0;
						}
					}
					IL_128:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_13E;
					}
				}
				IL_B0:
				goto IL_14D;
				IL_13E:
				if (false)
				{
				}
			}
			catch
			{
				this.ᜆ();
				throw;
			}
			IL_14D:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002874 File Offset: 0x00000A74
		[DebuggerHidden]
		ᜀ IEnumerator<!0>.ᜅ()
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
			return this.ᜀ;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028B8 File Offset: 0x00000AB8
		[DebuggerHidden]
		void IEnumerator.ᜂ()
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
			throw new NotSupportedException();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028F8 File Offset: 0x00000AF8
		void IDisposable.ᜆ()
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
				switch (this.ᜁ)
				{
				case 1:
				case 2:
					try
					{
						return;
					}
					finally
					{
						this.ᜀ();
					}
					break;
				}
				break;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002968 File Offset: 0x00000B68
		[DebuggerHidden]
		object IEnumerator.ᜄ()
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
			return this.ᜀ;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029B0 File Offset: 0x00000BB0
		[DebuggerHidden]
		public ᜀ(int A_0)
		{
			this.ᜁ = A_0;
			this.ᜂ = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029DC File Offset: 0x00000BDC
		private void ᜀ()
		{
			for (;;)
			{
				IL_3A:
				this.ᜁ = -1;
				this.ᜇ = (this.ᜆ as IDisposable);
				for (;;)
				{
					IL_52:
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								this.ᜇ.Dispose();
								num = 1;
								continue;
							case 1:
								return;
							case 2:
								if (this.ᜇ != null)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								return;
							}
							goto IL_3A;
						}
					}
				}
			}
		}

		// Token: 0x04000007 RID: 7
		private ᜀ ᜀ;

		// Token: 0x04000008 RID: 8
		private int ᜁ;

		// Token: 0x04000009 RID: 9
		private int ᜂ;

		// Token: 0x0400000A RID: 10
		public IEnumerable ᜃ;

		// Token: 0x0400000B RID: 11
		public IEnumerable ᜄ;

		// Token: 0x0400000C RID: 12
		public ᜀ ᜅ;

		// Token: 0x0400000D RID: 13
		public IEnumerator ᜆ;

		// Token: 0x0400000E RID: 14
		public IDisposable ᜇ;
	}

	// Token: 0x02000009 RID: 9
	[CompilerGenerated]
	private sealed class ᜁ<ᜀ> : IEnumerable<!0>, IEnumerator<!0>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002A74 File Offset: 0x00000C74
		[DebuggerHidden]
		IEnumerator<ᜀ> IEnumerable<!0>.ᜀ()
		{
			int num = 5;
			spr\u17C5<ᜀ>.ᜁ ᜁ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 4;
					continue;
				case 1:
					goto IL_77;
				case 2:
					goto IL_8A;
				case 3:
					this.ᜁ = 0;
					ᜁ = this;
					num = 2;
					continue;
				case 4:
					if (this.ᜁ == -2)
					{
						num = 3;
						continue;
					}
					goto IL_4C;
				}
				goto IL_28;
				IL_3A:
				num = 0;
				continue;
				IL_28:
				if (Thread.CurrentThread.ManagedThreadId == this.ᜂ)
				{
					goto IL_3A;
				}
				IL_4C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					if (false)
					{
					}
					ᜁ = new spr\u17C5<ᜀ>.ᜁ(0);
					num = 1;
					break;
				}
			}
			IL_77:
			IL_8A:
			ᜁ.ᜃ = this.ᜄ;
			return ᜁ;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B48 File Offset: 0x00000D48
		[DebuggerHidden]
		IEnumerator IEnumerable.ᜂ()
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
			return this.ᜀ();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B8C File Offset: 0x00000D8C
		bool IEnumerator.ᜆ()
		{
			for (;;)
			{
				int num = this.ᜁ;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				}
				if (false)
				{
				}
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return false;
					case 1:
						goto IL_74;
					case 2:
						num2 = 0;
						continue;
					case 3:
						switch (num)
						{
						case 0:
							goto IL_76;
						case 1:
							this.ᜁ = -1;
							num2 = 1;
							continue;
						default:
							num2 = 2;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_74:
			return false;
			IL_76:
			if (true)
			{
			}
			this.ᜁ = -1;
			this.ᜀ = (ᜀ)((object)this.ᜃ);
			this.ᜁ = 1;
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C48 File Offset: 0x00000E48
		[DebuggerHidden]
		ᜀ IEnumerator<!0>.ᜄ()
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
			return this.ᜀ;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C8C File Offset: 0x00000E8C
		[DebuggerHidden]
		void IEnumerator.ᜁ()
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
			throw new NotSupportedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CCC File Offset: 0x00000ECC
		void IDisposable.ᜅ()
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
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D08 File Offset: 0x00000F08
		[DebuggerHidden]
		object IEnumerator.ᜃ()
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
			return this.ᜀ;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002D50 File Offset: 0x00000F50
		[DebuggerHidden]
		public ᜁ(int A_0)
		{
			this.ᜁ = A_0;
			this.ᜂ = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x0400000F RID: 15
		private ᜀ ᜀ;

		// Token: 0x04000010 RID: 16
		private int ᜁ;

		// Token: 0x04000011 RID: 17
		private int ᜂ;

		// Token: 0x04000012 RID: 18
		public object ᜃ;

		// Token: 0x04000013 RID: 19
		public object ᜄ;
	}
}
