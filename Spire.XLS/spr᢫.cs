using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200051A RID: 1306
internal class spr\u18AB
{
	// Token: 0x06004F52 RID: 20306 RVA: 0x002FFDF4 File Offset: 0x002FEDF4
	public void ᜀ(XlsShape A_0)
	{
		int a_ = 10;
		int num = 2;
		Dictionary<Type, object> dictionary;
		for (;;)
		{
			int instance;
			switch (num)
			{
			case 0:
				goto IL_4A;
			case 1:
				IL_90:
				dictionary = new Dictionary<Type, object>();
				this.ᜀ[instance] = dictionary;
				num = 4;
				continue;
			case 3:
				if (!this.ᜀ.TryGetValue(instance, out dictionary))
				{
					num = 1;
					continue;
				}
				goto IL_A6;
			case 4:
				goto IL_A6;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			instance = A_0.Instance;
			num = 3;
			continue;
			IL_A6:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_90;
			default:
				goto IL_BC;
			}
		}
		IL_4A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁╃㙅ⵇ", a_));
		IL_BC:
		if (false)
		{
		}
		dictionary[A_0.GetType()] = null;
	}

	// Token: 0x06004F53 RID: 20307 RVA: 0x002FFED0 File Offset: 0x002FEED0
	public IEnumerable ᜀ()
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
		spr\u18AB.ᜀ ᜀ = new spr\u18AB.ᜀ(-2);
		ᜀ.ᜃ = this;
		return ᜀ;
	}

	// Token: 0x040023C6 RID: 9158
	private Dictionary<int, Dictionary<Type, object>> ᜀ = new Dictionary<int, Dictionary<Type, object>>();

	// Token: 0x0200051B RID: 1307
	[CompilerGenerated]
	private sealed class ᜀ : IEnumerable<object>, IEnumerator<object>
	{
		// Token: 0x06004F55 RID: 20309 RVA: 0x002FFF3C File Offset: 0x002FEF3C
		[DebuggerHidden]
		IEnumerator<object> IEnumerable<object>.ᜆ()
		{
			int num = 3;
			spr\u18AB.ᜀ ᜀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return ᜀ;
				case 1:
					if (true)
					{
					}
					num = 5;
					continue;
				case 2:
					return ᜀ;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜁ = 0;
						ᜀ = this;
						num = 2;
						continue;
					}
					break;
				case 5:
					if (this.ᜁ == -2)
					{
						num = 4;
						continue;
					}
					goto IL_4C;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.ᜂ)
				{
					num = 1;
					continue;
				}
				IL_4C:
				ᜀ = new spr\u18AB.ᜀ(0);
				ᜀ.ᜃ = this.ᜃ;
				num = 0;
			}
			return ᜀ;
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x00300010 File Offset: 0x002FF010
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
			return this.ᜆ();
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x00300054 File Offset: 0x002FF054
		bool IEnumerator.ᜈ()
		{
			bool result;
			try
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
							goto IL_214;
						case 1:
							if (!this.ᜈ.MoveNext())
							{
								num2 = 8;
								continue;
							}
							this.ᜆ = this.ᜈ.Current;
							this.ᜀ = new KeyValuePair<int, Type>(this.ᜄ, this.ᜆ);
							this.ᜁ = 3;
							result = true;
							num2 = 0;
							continue;
						case 2:
							this.ᜁ();
							num2 = 5;
							continue;
						case 3:
							goto IL_E3;
						case 4:
							if (num != 0)
							{
								num2 = 6;
								continue;
							}
							this.ᜁ = -1;
							this.ᜇ = this.ᜃ.ᜀ.Keys.GetEnumerator();
							this.ᜁ = 1;
							num2 = 13;
							continue;
						case 5:
							goto IL_216;
						case 6:
							num2 = 11;
							continue;
						case 7:
							goto IL_216;
						case 8:
							this.ᜀ();
							num2 = 9;
							continue;
						case 9:
							goto IL_9C;
						case 10:
							if (!this.ᜇ.MoveNext())
							{
								num2 = 2;
								continue;
							}
							this.ᜄ = this.ᜇ.Current;
							this.ᜅ = this.ᜃ.ᜀ[this.ᜄ];
							this.ᜈ = this.ᜅ.Keys.GetEnumerator();
							this.ᜁ = 2;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_78;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						case 11:
							if (num != 3)
							{
								num2 = 12;
								continue;
							}
							goto IL_78;
						case 12:
							num2 = 7;
							continue;
						case 13:
							goto IL_9C;
						case 14:
							goto IL_223;
						case 15:
							goto IL_E3;
						}
						break;
						IL_78:
						this.ᜁ = 2;
						num2 = 15;
						continue;
						IL_9C:
						num2 = 10;
						continue;
						IL_E3:
						num2 = 1;
						continue;
						IL_216:
						result = false;
						num2 = 14;
					}
				}
				IL_214:
				IL_223:;
			}
			catch
			{
				this.ᜇ();
				throw;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x003002BC File Offset: 0x002FF2BC
		[DebuggerHidden]
		object IEnumerator<object>.ᜂ()
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

		// Token: 0x06004F59 RID: 20313 RVA: 0x00300300 File Offset: 0x002FF300
		[DebuggerHidden]
		void IEnumerator.ᜃ()
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

		// Token: 0x06004F5A RID: 20314 RVA: 0x00300340 File Offset: 0x002FF340
		void IDisposable.ᜇ()
		{
			for (;;)
			{
				switch (this.ᜁ)
				{
				case 1:
				case 2:
				case 3:
					try
					{
						for (;;)
						{
							int num = this.ᜁ;
							if (true)
							{
							}
							int num2 = 2;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									num2 = 1;
									continue;
								case 1:
									goto IL_79;
								case 2:
									switch (num)
									{
									case 2:
									case 3:
										try
										{
										}
										finally
										{
											this.ᜀ();
										}
										goto IL_79;
									default:
										num2 = 0;
										continue;
									}
									break;
								case 3:
									goto IL_81;
								}
								break;
								IL_79:
								num2 = 3;
							}
						}
						IL_81:
						goto IL_8B;
					}
					finally
					{
						this.ᜁ();
					}
					break;
					IL_8B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_A1;
					}
					break;
				}
				break;
			}
			return;
			IL_A1:
			if (false)
			{
			}
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0030041C File Offset: 0x002FF41C
		[DebuggerHidden]
		object IEnumerator.ᜅ()
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

		// Token: 0x06004F5C RID: 20316 RVA: 0x00300460 File Offset: 0x002FF460
		[DebuggerHidden]
		public ᜀ(int A_0)
		{
			this.ᜁ = A_0;
			this.ᜂ = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0030048C File Offset: 0x002FF48C
		private void ᜁ()
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
			this.ᜁ = -1;
			((IDisposable)this.ᜇ).Dispose();
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x003004E0 File Offset: 0x002FF4E0
		private void ᜀ()
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
			this.ᜁ = 1;
			((IDisposable)this.ᜈ).Dispose();
		}

		// Token: 0x040023C7 RID: 9159
		private object ᜀ;

		// Token: 0x040023C8 RID: 9160
		private int ᜁ;

		// Token: 0x040023C9 RID: 9161
		private int ᜂ;

		// Token: 0x040023CA RID: 9162
		public spr\u18AB ᜃ;

		// Token: 0x040023CB RID: 9163
		public int ᜄ;

		// Token: 0x040023CC RID: 9164
		public Dictionary<Type, object> ᜅ;

		// Token: 0x040023CD RID: 9165
		public Type ᜆ;

		// Token: 0x040023CE RID: 9166
		public Dictionary<int, Dictionary<Type, object>>.KeyCollection.Enumerator ᜇ;

		// Token: 0x040023CF RID: 9167
		public Dictionary<Type, object>.KeyCollection.Enumerator ᜈ;
	}
}
