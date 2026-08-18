using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200040A RID: 1034
internal class sprᝐ
{
	// Token: 0x06003E54 RID: 15956 RVA: 0x00229324 File Offset: 0x00228324
	public static IEnumerable<ᜀ> ᜀ<ᜀ, ᜁ>(IEnumerable<ᜁ> A_0) where ᜁ : ᜀ
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
		sprᝐ<ᜀ, ᜁ>.ᜀ ᜀ = new sprᝐ<ᜀ, ᜁ>.ᜀ(-2);
		ᜀ.ᜄ = A_0;
		return ᜀ;
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x00229370 File Offset: 0x00228370
	public static string ᜀ(string A_0)
	{
		while (string.IsNullOrEmpty(A_0))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0;
		}
		return sprᝐ.ᜀ.Replace(A_0, new MatchEvaluator(sprᝐ.ᜀ));
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x002293D0 File Offset: 0x002283D0
	private static string ᜀ(Match A_0)
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
		string value = A_0.Groups[1].Value;
		char c = (char)int.Parse(value, NumberStyles.HexNumber);
		return new string(new char[]
		{
			c
		});
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x00229450 File Offset: 0x00228450
	// Note: this type is marked as 'beforefieldinit'.
	static sprᝐ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		sprᝐ.ᜀ = new Regex(RecordTableEnumerator.b("Ṁ㭂浄᱆ᕈ⽊ⱌ扎㝐ቒ硔ᅖј⁚楜≞䡠㱢", a_));
	}

	// Token: 0x04001AB1 RID: 6833
	private static Regex ᜀ;

	// Token: 0x0200040B RID: 1035
	[CompilerGenerated]
	private sealed class ᜀ<ᜀ, ᜁ> : IEnumerable<ᜀ>, IEnumerator<ᜀ> where ᜁ : ᜀ
	{
		// Token: 0x06003E59 RID: 15961 RVA: 0x002294AC File Offset: 0x002284AC
		[DebuggerHidden]
		IEnumerator<ᜀ> IEnumerable<!0>.ᜅ()
		{
			int num = 5;
			sprᝐ<ᜀ, ᜁ>.ᜀ ᜀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ = 0;
					ᜀ = this;
					num = 1;
					continue;
				case 1:
					goto IL_97;
				case 2:
					if (this.ᜁ == -2)
					{
						num = 0;
						continue;
					}
					goto IL_4C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3A;
					}
					goto Block_1;
				case 4:
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_28;
				IL_3A:
				num = 4;
				continue;
				IL_28:
				if (Thread.CurrentThread.ManagedThreadId == this.ᜂ)
				{
					goto IL_3A;
				}
				IL_4C:
				ᜀ = new sprᝐ<ᜀ, ᜁ>.ᜀ(0);
				num = 3;
			}
			Block_1:
			if (false)
			{
			}
			IL_97:
			ᜀ.ᜃ = this.ᜄ;
			return ᜀ;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00229584 File Offset: 0x00228584
		[DebuggerHidden]
		IEnumerator IEnumerable.ᜄ()
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
			return this.ᜅ();
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x002295C8 File Offset: 0x002285C8
		bool IEnumerator.ᜇ()
		{
			bool result;
			try
			{
				for (;;)
				{
					int num = this.ᜁ;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_13C;
						case 1:
							goto IL_3F;
						case 2:
							this.ᜀ();
							num2 = 7;
							continue;
						case 3:
							if (!this.ᜆ.MoveNext())
							{
								num2 = 2;
								continue;
							}
							this.ᜅ = (ᜀ)((object)this.ᜆ.Current);
							this.ᜀ = this.ᜅ;
							this.ᜁ = 2;
							result = true;
							num2 = 6;
							continue;
						case 4:
							goto IL_5B;
						case 5:
							goto IL_149;
						case 6:
							goto IL_B5;
						case 7:
							goto IL_13C;
						case 8:
							num2 = 0;
							continue;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3F;
							default:
								if (false)
								{
								}
								goto IL_5B;
							}
							break;
						}
						break;
						IL_3F:
						switch (num)
						{
						case 0:
							this.ᜁ = -1;
							this.ᜆ = this.ᜃ.GetEnumerator();
							this.ᜁ = 1;
							num2 = 9;
							continue;
						case 1:
							IL_13C:
							result = false;
							num2 = 5;
							continue;
						case 2:
							this.ᜁ = 1;
							num2 = 4;
							continue;
						default:
							num2 = 8;
							continue;
						}
						IL_5B:
						num2 = 3;
					}
				}
				IL_B5:
				IL_149:;
			}
			catch
			{
				this.ᜁ();
				throw;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00229758 File Offset: 0x00228758
		[DebuggerHidden]
		ᜀ IEnumerator<!0>.ᜃ()
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

		// Token: 0x06003E5D RID: 15965 RVA: 0x0022979C File Offset: 0x0022879C
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

		// Token: 0x06003E5E RID: 15966 RVA: 0x002297DC File Offset: 0x002287DC
		void IDisposable.ᜁ()
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

		// Token: 0x06003E5F RID: 15967 RVA: 0x0022984C File Offset: 0x0022884C
		[DebuggerHidden]
		object IEnumerator.ᜆ()
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

		// Token: 0x06003E60 RID: 15968 RVA: 0x00229894 File Offset: 0x00228894
		[DebuggerHidden]
		public ᜀ(int A_0)
		{
			this.ᜁ = A_0;
			this.ᜂ = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x002298C0 File Offset: 0x002288C0
		private void ᜀ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_49:
				if (this.ᜆ == null)
				{
					return;
				}
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_30;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_49;
				case 1:
					goto IL_76;
				case 2:
					this.ᜆ.Dispose();
					num = 1;
					continue;
				}
				goto IL_30;
			}
			IL_76:
			return;
			IL_30:
			this.ᜁ = -1;
			num = 0;
			goto IL_1E;
		}

		// Token: 0x04001AB2 RID: 6834
		private ᜀ ᜀ;

		// Token: 0x04001AB3 RID: 6835
		private int ᜁ;

		// Token: 0x04001AB4 RID: 6836
		private int ᜂ;

		// Token: 0x04001AB5 RID: 6837
		public IEnumerable<ᜁ> ᜃ;

		// Token: 0x04001AB6 RID: 6838
		public IEnumerable<ᜁ> ᜄ;

		// Token: 0x04001AB7 RID: 6839
		public ᜀ ᜅ;

		// Token: 0x04001AB8 RID: 6840
		public IEnumerator<ᜁ> ᜆ;
	}
}
