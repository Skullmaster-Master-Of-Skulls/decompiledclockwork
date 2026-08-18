using System;
using System.Diagnostics;
using System.Threading;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000055 RID: 85
	public class XlsObject : IExcelApplication, IDisposable
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00056118 File Offset: 0x00055118
		internal spr\u1DF5 ReservedHandle
		{
			[DebuggerStepThrough]
			get
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
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0005615C File Offset: 0x0005515C
		public object Parent
		{
			[DebuggerStepThrough]
			get
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000561A0 File Offset: 0x000551A0
		internal spr\u17FF AppImplementation
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
				return (spr\u17FF)this.ᜀ;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000561E8 File Offset: 0x000551E8
		private XlsObject()
		{
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000561FC File Offset: 0x000551FC
		internal XlsObject(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 17;
			this..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("♆㥈㭊⅌♎㉐㉒⅔㹖㙘㕚", a_));
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈㥊⡌ⅎ═", a_));
			}
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00056258 File Offset: 0x00055258
		protected override void Finalize()
		{
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.Dispose();
			}
			finally
			{
				base.Finalize();
			}
			if (true)
			{
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000562B4 File Offset: 0x000552B4
		protected internal object FindParent(Type parentType)
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
			return XlsObject.FindParent(this.ᜁ, parentType);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000562FC File Offset: 0x000552FC
		protected internal object FindParent(Type parentType, bool bSubTypes)
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
			return XlsObject.FindParent(this.ᜁ, parentType, bSubTypes);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00056344 File Offset: 0x00055344
		protected internal static object FindParent(object parentStart, Type parentType)
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
			return XlsObject.FindParent(parentStart, parentType, true);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00056388 File Offset: 0x00055388
		protected internal static object FindParent(object parentStart, Type parentType, bool bSubTypes)
		{
			int a_ = 9;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_17:
					int num = 11;
					for (;;)
					{
						bool isInterface;
						int num2;
						IExcelApplication excelApplication;
						switch (num)
						{
						case 0:
							if (!isInterface)
							{
								num = 6;
								continue;
							}
							num = 12;
							continue;
						case 1:
							num = 18;
							continue;
						case 2:
							if (num2 > 100)
							{
								num = 19;
								continue;
							}
							num = 7;
							continue;
						case 3:
						{
							Type type;
							if (!type.Equals(parentType))
							{
								num = 17;
								continue;
							}
							return excelApplication;
						}
						case 4:
							return excelApplication;
						case 5:
							if (excelApplication == null)
							{
								num = 16;
								continue;
							}
							goto IL_F5;
						case 6:
							num = 3;
							continue;
						case 7:
							if (excelApplication != null)
							{
								num = 15;
								continue;
							}
							return excelApplication;
						case 8:
							goto IL_F5;
						case 9:
							goto IL_88;
						case 10:
							goto IL_18C;
						case 12:
						{
							Type type;
							if (type.GetInterface(parentType.Name, false) == null)
							{
								num = 10;
								continue;
							}
							return excelApplication;
						}
						case 13:
							num = 4;
							continue;
						case 14:
							if (bSubTypes)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_18C;
						case 15:
						{
							Type type = excelApplication.GetType();
							num = 0;
							continue;
						}
						case 16:
							return excelApplication;
						case 17:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 18:
						{
							Type type;
							if (type.IsSubclassOf(parentType))
							{
								num = 13;
								continue;
							}
							goto IL_18C;
						}
						case 19:
							goto IL_115;
						}
						if (parentType == null)
						{
							num = 9;
							continue;
						}
						num2 = 0;
						excelApplication = (IExcelApplication)parentStart;
						isInterface = parentType.IsInterface;
						num = 8;
						continue;
						IL_F5:
						num = 2;
						continue;
						IL_18C:
						excelApplication = (IExcelApplication)excelApplication.Parent;
						num2++;
						num = 5;
					}
				}
				IL_88:
				throw new ArgumentNullException(RecordTableEnumerator.b("伾⁀ㅂ⁄⥆㵈Ὂ㑌㽎㑐", a_));
				IL_115:
				throw new ArgumentException(RecordTableEnumerator.b("匾⡀ⵂ⹄㑆楈⡊㑌ⱎ㵐㙒畔㍖㱘⽚㡜㱞ᕠ٢Ť䥦", a_));
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000565DC File Offset: 0x000555DC
		protected internal object[] FindParents(Type[] arrTypes)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					IExcelApplication excelApplication = (IExcelApplication)this.ᜁ;
					object[] array = new object[arrTypes.Length];
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_E1;
						case 1:
						{
							int num3;
							array[num3] = excelApplication;
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						case 2:
							goto IL_A8;
						case 3:
							return array;
						case 4:
							if (excelApplication == null)
							{
								num2 = 3;
								continue;
							}
							goto IL_A8;
						case 5:
						{
							int num3;
							if (num3 != -1)
							{
								num2 = 1;
								continue;
							}
							goto IL_E3;
						}
						case 6:
							goto IL_E3;
						case 7:
							if (excelApplication != null)
							{
								num2 = 9;
								continue;
							}
							return array;
						case 8:
							if (num <= 100)
							{
								num2 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A8;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 9:
						{
							int num3 = Array.IndexOf<Type>(arrTypes, excelApplication.GetType());
							num2 = 5;
							continue;
						}
						}
						break;
						IL_A8:
						num2 = 8;
						continue;
						IL_E3:
						excelApplication = (IExcelApplication)excelApplication.Parent;
						num++;
						num2 = 4;
					}
				}
				IL_E1:
				throw new ArgumentException(RecordTableEnumerator.b("倻圽⸿⥁㝃晅⭇㍉⽋≍㕏牑こ㍕ⱗ㽙㽛⩝՟١䩣", a_));
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0005673C File Offset: 0x0005573C
		protected internal object FindParent(Type[] arrTypes)
		{
			int a_ = 13;
			IExcelApplication excelApplication;
			for (;;)
			{
				int num = 0;
				excelApplication = (IExcelApplication)this.Parent;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (excelApplication != null)
						{
							num2 = 1;
							continue;
						}
						return excelApplication;
					case 1:
					{
						if (true)
						{
						}
						int num3 = Array.IndexOf<Type>(arrTypes, excelApplication.GetType());
						num2 = 3;
						continue;
					}
					case 2:
						goto IL_C3;
					case 3:
					{
						int num3;
						if (num3 != -1)
						{
							num2 = 8;
							continue;
						}
						excelApplication = (IExcelApplication)excelApplication.Parent;
						num++;
						num2 = 4;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return excelApplication;
						default:
							if (false)
							{
							}
							if (excelApplication == null)
							{
								num2 = 2;
								continue;
							}
							goto IL_67;
						}
						break;
					case 5:
						goto IL_67;
					case 6:
						if (num > 100)
						{
							num2 = 7;
							continue;
						}
						num2 = 0;
						continue;
					case 7:
						goto IL_7C;
					case 8:
						goto IL_115;
					}
					break;
					IL_67:
					num2 = 6;
				}
			}
			return excelApplication;
			IL_7C:
			throw new ArgumentException(RecordTableEnumerator.b("⽂ⱄ⥆≈㡊浌ⱎ⡐げ㥔㉖祘㽚㡜⭞Ѡbᅤɦ൨䕪", a_));
			IL_C3:
			return excelApplication;
			IL_115:
			return excelApplication;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00056864 File Offset: 0x00055864
		protected internal void SetParent(object parent)
		{
			int a_ = 18;
			if (true)
			{
			}
			if (parent == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
			}
			this.ᜁ = parent;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000568C8 File Offset: 0x000558C8
		protected void CheckDisposed()
		{
			int a_ = 13;
			if (this.m_bIsDisposed)
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
					break;
				}
				throw new ApplicationException(RecordTableEnumerator.b("B⩄⩆㥈⑊⍌⩎㽐❒畔㽖㡘⡚絜㵞Ѡ٢୤䝦൨ɪṬὮṰrၴ፶坸", a_));
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0005692C File Offset: 0x0005592C
		[DebuggerStepThrough]
		protected internal virtual int AddReference()
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
			return Interlocked.Increment(ref this.ᜂ);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00056974 File Offset: 0x00055974
		[DebuggerStepThrough]
		protected internal virtual int ReleaseReference()
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
			return Interlocked.Decrement(ref this.ᜂ);
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000569BC File Offset: 0x000559BC
		protected internal int ReferenceCount
		{
			[DebuggerStepThrough]
			get
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
				return this.ᜂ;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00056A00 File Offset: 0x00055A00
		public virtual void Dispose()
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					}
					goto Block_2;
				}
				if (!this.m_bIsDisposed)
				{
					num = 0;
					continue;
				}
				return;
				IL_34:
				this.OnDispose();
				this.m_bIsDisposed = true;
				GC.SuppressFinalize(this);
				num = 1;
			}
			Block_2:
			if (false)
			{
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00056A88 File Offset: 0x00055A88
		protected virtual void OnDispose()
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
		}

		// Token: 0x04000175 RID: 373
		private spr\u1DF5 ᜀ;

		// Token: 0x04000176 RID: 374
		private object ᜁ;

		// Token: 0x04000177 RID: 375
		private int \u2460\u00B0\u0085\u009B;

		// Token: 0x04000178 RID: 376
		private bool[] \u25D9\u00AC\u0080\u0098;

		// Token: 0x04000179 RID: 377
		private int \u2460\u0092\u00A8\u0091;

		// Token: 0x0400017A RID: 378
		private int ᜂ;

		// Token: 0x0400017B RID: 379
		private int \u25D8\u0095\u0096\u00A1;

		// Token: 0x0400017C RID: 380
		private long[] \u2609\u0086\u0085\u0092;

		// Token: 0x0400017D RID: 381
		private int \u2609\u0080\u00A5\u008B;

		// Token: 0x0400017E RID: 382
		protected bool m_bIsDisposed;
	}
}
