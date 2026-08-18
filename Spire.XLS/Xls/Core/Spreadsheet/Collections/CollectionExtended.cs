using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000101 RID: 257
	[DebuggerStepThrough]
	public class CollectionExtended<T> : CollectionBase<T>, IList<T>, IExcelApplication, ICloneParent
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00072160 File Offset: 0x00071160
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000721A4 File Offset: 0x000711A4
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x000721E8 File Offset: 0x000711E8
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0007222C File Offset: 0x0007122C
		internal bool QuietMode
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
				return this.ᜂ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ = value;
						num = 2;
						continue;
					case 2:
						goto IL_40;
					}
					IL_1C:
					if (value != this.ᜂ)
					{
						num = 0;
						continue;
					}
					IL_40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_56;
					}
				}
				IL_56:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000722A8 File Offset: 0x000712A8
		internal spr\u17FF AppImplementation
		{
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
				return (spr\u17FF)this.ReservedHandle;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000B95 RID: 2965 RVA: 0x000722F0 File Offset: 0x000712F0
		// (remove) Token: 0x06000B96 RID: 2966 RVA: 0x00072384 File Offset: 0x00071384
		public event EventHandler Changed
		{
			add
			{
				for (;;)
				{
					EventHandler eventHandler = this.ᜃ;
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							if (eventHandler == eventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 1:
							goto IL_2D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_2D:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value2, eventHandler2);
						num = 0;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					EventHandler eventHandler = this.ᜃ;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
							if (eventHandler == eventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_2D:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value2, eventHandler2);
						num = 1;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000B97 RID: 2967 RVA: 0x00072418 File Offset: 0x00071418
		// (remove) Token: 0x06000B98 RID: 2968 RVA: 0x000724AC File Offset: 0x000714AC
		internal event CollectionExtended<T>.ᜂ Clearing
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜂ ᜂ = this.ᜄ;
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜂ ᜂ2;
						switch (num)
						{
						case 0:
							goto IL_25;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							if (ᜂ == ᜂ2)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_25;
						}
						break;
						IL_25:
						ᜂ2 = ᜂ;
						CollectionExtended<T>.ᜂ value2 = (CollectionExtended<T>.ᜂ)Delegate.Combine(ᜂ2, value);
						ᜂ = Interlocked.CompareExchange<CollectionExtended<T>.ᜂ>(ref this.ᜄ, value2, ᜂ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					if (true)
					{
					}
					CollectionExtended<T>.ᜂ ᜂ = this.ᜄ;
					int num = 1;
					for (;;)
					{
						CollectionExtended<T>.ᜂ ᜂ2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 1:
							goto IL_2D;
						case 2:
							if (ᜂ == ᜂ2)
							{
								num = 0;
								continue;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜂ2 = ᜂ;
						CollectionExtended<T>.ᜂ value2 = (CollectionExtended<T>.ᜂ)Delegate.Remove(ᜂ2, value);
						ᜂ = Interlocked.CompareExchange<CollectionExtended<T>.ᜂ>(ref this.ᜄ, value2, ᜂ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000B99 RID: 2969 RVA: 0x00072540 File Offset: 0x00071540
		// (remove) Token: 0x06000B9A RID: 2970 RVA: 0x000725D4 File Offset: 0x000715D4
		internal event CollectionExtended<T>.ᜂ Cleared
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜂ ᜂ = this.ᜅ;
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜂ ᜂ2;
						switch (num)
						{
						case 0:
							goto IL_25;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_76;
							}
							break;
						case 2:
							if (ᜂ == ᜂ2)
							{
								num = 1;
								continue;
							}
							goto IL_25;
						}
						break;
						IL_25:
						ᜂ2 = ᜂ;
						CollectionExtended<T>.ᜂ value2 = (CollectionExtended<T>.ᜂ)Delegate.Combine(ᜂ2, value);
						ᜂ = Interlocked.CompareExchange<CollectionExtended<T>.ᜂ>(ref this.ᜅ, value2, ᜂ2);
						num = 2;
					}
				}
				IL_76:
				if (true)
				{
				}
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜂ ᜂ = this.ᜅ;
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						CollectionExtended<T>.ᜂ ᜂ2;
						switch (num)
						{
						case 0:
							if (ᜂ == ᜂ2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 1:
							goto IL_2D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_2D:
						ᜂ2 = ᜂ;
						CollectionExtended<T>.ᜂ value2 = (CollectionExtended<T>.ᜂ)Delegate.Remove(ᜂ2, value);
						ᜂ = Interlocked.CompareExchange<CollectionExtended<T>.ᜂ>(ref this.ᜅ, value2, ᜂ2);
						num = 0;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000B9B RID: 2971 RVA: 0x00072668 File Offset: 0x00071668
		// (remove) Token: 0x06000B9C RID: 2972 RVA: 0x000726FC File Offset: 0x000716FC
		internal event CollectionExtended<T>.ᜀ Inserting
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜆ;
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							goto IL_2D;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							if (ᜀ == ᜀ2)
							{
								num = 1;
								continue;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Combine(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜆ, value2, ᜀ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜆ;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							if (ᜀ == ᜀ2)
							{
								num = 1;
								continue;
							}
							goto IL_2D;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Remove(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜆ, value2, ᜀ2);
						num = 0;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000B9D RID: 2973 RVA: 0x00072790 File Offset: 0x00071790
		// (remove) Token: 0x06000B9E RID: 2974 RVA: 0x00072824 File Offset: 0x00071824
		internal event CollectionExtended<T>.ᜀ Inserted
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜇ;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							if (ᜀ == ᜀ2)
							{
								num = 1;
								continue;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Combine(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜇ, value2, ᜀ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜇ;
					int num = 2;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_7E;
							}
							break;
						case 1:
							if (ᜀ == ᜀ2)
							{
								num = 0;
								continue;
							}
							goto IL_25;
						case 2:
							goto IL_25;
						}
						break;
						IL_25:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Remove(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜇ, value2, ᜀ2);
						if (true)
						{
						}
						num = 1;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000B9F RID: 2975 RVA: 0x000728B8 File Offset: 0x000718B8
		// (remove) Token: 0x06000BA0 RID: 2976 RVA: 0x0007294C File Offset: 0x0007194C
		internal event CollectionExtended<T>.ᜀ Removing
		{
			add
			{
				if (true)
				{
				}
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜈ;
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							if (ᜀ == ᜀ2)
							{
								num = 1;
								continue;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Combine(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜈ, value2, ᜀ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜈ;
					int num = 1;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							if (ᜀ == ᜀ2)
							{
								num = 2;
								continue;
							}
							goto IL_25;
						case 1:
							goto IL_25;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_76;
							}
							break;
						}
						break;
						IL_25:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Remove(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜈ, value2, ᜀ2);
						num = 0;
					}
				}
				IL_76:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000BA1 RID: 2977 RVA: 0x000729E0 File Offset: 0x000719E0
		// (remove) Token: 0x06000BA2 RID: 2978 RVA: 0x00072A74 File Offset: 0x00071A74
		internal event CollectionExtended<T>.ᜀ Removed
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜉ;
					int num = 1;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						case 1:
							if (true)
							{
							}
							goto IL_2D;
						case 2:
							if (ᜀ == ᜀ2)
							{
								num = 0;
								continue;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Combine(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜉ, value2, ᜀ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜀ ᜀ = this.ᜉ;
					int num = 2;
					for (;;)
					{
						CollectionExtended<T>.ᜀ ᜀ2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_76;
							}
							break;
						case 1:
							if (ᜀ == ᜀ2)
							{
								num = 0;
								continue;
							}
							goto IL_25;
						case 2:
							goto IL_25;
						}
						break;
						IL_25:
						ᜀ2 = ᜀ;
						CollectionExtended<T>.ᜀ value2 = (CollectionExtended<T>.ᜀ)Delegate.Remove(ᜀ2, value);
						ᜀ = Interlocked.CompareExchange<CollectionExtended<T>.ᜀ>(ref this.ᜉ, value2, ᜀ2);
						num = 1;
					}
				}
				IL_76:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000BA3 RID: 2979 RVA: 0x00072B08 File Offset: 0x00071B08
		// (remove) Token: 0x06000BA4 RID: 2980 RVA: 0x00072B9C File Offset: 0x00071B9C
		internal event CollectionExtended<T>.ᜁ Setting
		{
			add
			{
				for (;;)
				{
					if (true)
					{
					}
					CollectionExtended<T>.ᜁ ᜁ = this.ᜊ;
					int num = 1;
					for (;;)
					{
						CollectionExtended<T>.ᜁ ᜁ2;
						switch (num)
						{
						case 0:
							if (ᜁ == ᜁ2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 1:
							goto IL_2D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_2D:
						ᜁ2 = ᜁ;
						CollectionExtended<T>.ᜁ value2 = (CollectionExtended<T>.ᜁ)Delegate.Combine(ᜁ2, value);
						ᜁ = Interlocked.CompareExchange<CollectionExtended<T>.ᜁ>(ref this.ᜊ, value2, ᜁ2);
						num = 0;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜁ ᜁ = this.ᜊ;
					int num = 0;
					for (;;)
					{
						CollectionExtended<T>.ᜁ ᜁ2;
						switch (num)
						{
						case 0:
							goto IL_25;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_7E;
							}
							break;
						case 2:
							if (ᜁ == ᜁ2)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_25;
						}
						break;
						IL_25:
						ᜁ2 = ᜁ;
						CollectionExtended<T>.ᜁ value2 = (CollectionExtended<T>.ᜁ)Delegate.Remove(ᜁ2, value);
						ᜁ = Interlocked.CompareExchange<CollectionExtended<T>.ᜁ>(ref this.ᜊ, value2, ᜁ2);
						num = 2;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000BA5 RID: 2981 RVA: 0x00072C30 File Offset: 0x00071C30
		// (remove) Token: 0x06000BA6 RID: 2982 RVA: 0x00072CC4 File Offset: 0x00071CC4
		internal event CollectionExtended<T>.ᜁ Set
		{
			add
			{
				for (;;)
				{
					CollectionExtended<T>.ᜁ ᜁ = this.ᜋ;
					int num = 1;
					for (;;)
					{
						CollectionExtended<T>.ᜁ ᜁ2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25;
							default:
								goto IL_76;
							}
							break;
						case 1:
							goto IL_25;
						case 2:
							if (ᜁ == ᜁ2)
							{
								num = 0;
								continue;
							}
							goto IL_25;
						}
						break;
						IL_25:
						ᜁ2 = ᜁ;
						CollectionExtended<T>.ᜁ value2 = (CollectionExtended<T>.ᜁ)Delegate.Combine(ᜁ2, value);
						ᜁ = Interlocked.CompareExchange<CollectionExtended<T>.ᜁ>(ref this.ᜋ, value2, ᜁ2);
						num = 2;
					}
				}
				IL_76:
				if (true)
				{
				}
				if (false)
				{
				}
			}
			remove
			{
				for (;;)
				{
					CollectionExtended<T>.ᜁ ᜁ = this.ᜋ;
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						CollectionExtended<T>.ᜁ ᜁ2;
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
							if (ᜁ == ᜁ2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D;
							default:
								goto IL_7E;
							}
							break;
						}
						break;
						IL_2D:
						ᜁ2 = ᜁ;
						CollectionExtended<T>.ᜁ value2 = (CollectionExtended<T>.ᜁ)Delegate.Remove(ᜁ2, value);
						ᜁ = Interlocked.CompareExchange<CollectionExtended<T>.ᜁ>(ref this.ᜋ, value2, ᜁ2);
						num = 1;
					}
				}
				IL_7E:
				if (false)
				{
				}
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00072D58 File Offset: 0x00071D58
		internal CollectionExtended()
		{
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00072D6C File Offset: 0x00071D6C
		internal CollectionExtended(spr\u1DF5 A_0, object A_1) : this()
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00072D90 File Offset: 0x00071D90
		private void ᜀ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					if (!this.ᜂ)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜃ(this, EventArgs.Empty);
						num = 1;
						continue;
					}
					break;
				}
				if (this.ᜃ == null)
				{
					break;
				}
				num = 3;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00072E38 File Offset: 0x00071E38
		protected override void OnClear()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜂ)
					{
						num = 1;
						continue;
					}
					goto IL_81;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						this.ᜄ();
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_49;
				case 3:
					num = 0;
					continue;
				}
				if (this.ᜄ == null)
				{
					break;
				}
				num = 3;
			}
			IL_49:
			IL_81:
			if (true)
			{
			}
			base.OnClear();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00072EE0 File Offset: 0x00071EE0
		protected override void OnClearComplete()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_49;
				case 2:
					if (!this.ᜂ)
					{
						num = 3;
						continue;
					}
					goto IL_93;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜅ();
						num = 1;
						continue;
					}
					break;
				case 4:
					num = 2;
					continue;
				}
				if (this.ᜅ == null)
				{
					break;
				}
				num = 4;
			}
			IL_49:
			IL_93:
			base.OnClearComplete();
			this.ᜀ();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00072F8C File Offset: 0x00071F8C
		protected override void OnInsert(int index, T value)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (false)
						{
						}
						this.ᜆ(this, new CollectionChangeEventArgs<T>(index, value));
						num = 4;
						continue;
					}
					break;
				case 2:
					if (!this.ᜂ)
					{
						num = 1;
						continue;
					}
					goto IL_9B;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_59;
				}
				if (this.ᜆ == null)
				{
					break;
				}
				if (true)
				{
				}
				num = 3;
			}
			IL_59:
			IL_9B:
			base.OnInsert(index, value);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0007303C File Offset: 0x0007203C
		protected override void OnInsertComplete(int index, T value)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (!this.ᜂ)
					{
						num = 2;
						continue;
					}
					goto IL_9B;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (false)
						{
						}
						this.ᜇ(this, new CollectionChangeEventArgs<T>(index, value));
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_51;
				case 4:
					num = 1;
					continue;
				}
				if (this.ᜇ == null)
				{
					break;
				}
				num = 4;
			}
			IL_51:
			IL_9B:
			base.OnInsertComplete(index, value);
			this.ᜀ();
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000730F4 File Offset: 0x000720F4
		protected override void OnRemove(int index, T value)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_51;
				case 2:
					if (!this.ᜂ)
					{
						num = 4;
						continue;
					}
					goto IL_9B;
				case 3:
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜈ(this, new CollectionChangeEventArgs<T>(index, value));
						num = 1;
						continue;
					}
					break;
				}
				if (this.ᜈ == null)
				{
					break;
				}
				num = 3;
			}
			IL_51:
			IL_9B:
			base.OnRemove(index, value);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000731A4 File Offset: 0x000721A4
		protected override void OnRemoveComplete(int index, T value)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜂ)
					{
						num = 4;
						continue;
					}
					goto IL_9B;
				case 2:
					goto IL_59;
				case 3:
					num = 0;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					default:
						if (false)
						{
						}
						this.ᜉ(this, new CollectionChangeEventArgs<T>(index, value));
						num = 2;
						continue;
					}
					break;
				}
				if (true)
				{
				}
				if (this.ᜉ == null)
				{
					break;
				}
				num = 3;
			}
			IL_59:
			IL_9B:
			base.OnRemoveComplete(index, value);
			this.ᜀ();
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0007325C File Offset: 0x0007225C
		protected override void OnSet(int index, T oldValue, T newValue)
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A0;
					default:
						if (false)
						{
						}
						this.ᜊ(index, oldValue, newValue);
						num = 4;
						continue;
					}
					break;
				case 2:
					if (!this.ᜂ)
					{
						num = 1;
						continue;
					}
					goto IL_A0;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_5E;
				}
				if (this.ᜊ == null)
				{
					break;
				}
				num = 3;
			}
			IL_5E:
			IL_A0:
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00073314 File Offset: 0x00072314
		protected override void OnSetComplete(int index, T oldValue, T newValue)
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
					if (!this.ᜂ)
					{
						num = 4;
						continue;
					}
					goto IL_A0;
				case 1:
					num = 0;
					continue;
				case 3:
					goto IL_56;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A0;
					default:
						if (false)
						{
						}
						this.ᜋ(index, oldValue, newValue);
						num = 3;
						continue;
					}
					break;
				}
				if (this.ᜋ == null)
				{
					break;
				}
				num = 1;
			}
			IL_56:
			IL_A0:
			base.OnSetComplete(index, oldValue, newValue);
			this.ᜀ();
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000733D0 File Offset: 0x000723D0
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
			return this.FindParent(parentType, true);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00073414 File Offset: 0x00072414
		protected internal object FindParent(Type parentType, bool bCheckSubclasses)
		{
			int a_ = 18;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					IExcelApplication excelApplication = (IExcelApplication)this.Parent;
					bool isInterface = parentType.IsInterface;
					int num2 = 15;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 11;
							continue;
						case 1:
							goto IL_16B;
						case 2:
							num2 = 18;
							continue;
						case 3:
							return excelApplication;
						case 4:
							return excelApplication;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_133;
							default:
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						case 6:
						{
							Type type;
							if (type.GetInterface(parentType.Name, false) == null)
							{
								goto IL_133;
							}
							return excelApplication;
						}
						case 7:
							if (excelApplication == null)
							{
								num2 = 3;
								continue;
							}
							goto IL_14B;
						case 8:
							if (!isInterface)
							{
								num2 = 17;
								continue;
							}
							num2 = 6;
							continue;
						case 9:
						{
							Type type;
							if (!type.Equals(parentType))
							{
								num2 = 2;
								continue;
							}
							return excelApplication;
						}
						case 10:
							goto IL_186;
						case 11:
						{
							Type type;
							if (type.IsSubclassOf(parentType))
							{
								num2 = 5;
								continue;
							}
							goto IL_186;
						}
						case 12:
							if (excelApplication.Parent != null)
							{
								num2 = 14;
								continue;
							}
							return excelApplication;
						case 13:
							num2 = 12;
							continue;
						case 14:
						{
							Type type = excelApplication.GetType();
							num2 = 8;
							continue;
						}
						case 15:
							goto IL_14B;
						case 16:
							if (num > 100)
							{
								num2 = 1;
								continue;
							}
							num2 = 19;
							continue;
						case 17:
							num2 = 9;
							continue;
						case 18:
							if (bCheckSubclasses)
							{
								num2 = 0;
								continue;
							}
							goto IL_186;
						case 19:
							if (excelApplication != null)
							{
								num2 = 13;
								continue;
							}
							return excelApplication;
						}
						break;
						IL_133:
						num2 = 10;
						continue;
						IL_14B:
						num2 = 16;
						continue;
						IL_186:
						excelApplication = (IExcelApplication)excelApplication.Parent;
						num++;
						num2 = 7;
					}
				}
				IL_16B:
				if (true)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("⑇⍉≋╍⍏牑㝓⽕㭗㙙㥛繝џݡၣͥ୧ṩ५੭兯", a_));
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00073668 File Offset: 0x00072668
		protected internal void SetParent(object parent)
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
			this.ᜁ = parent;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000736AC File Offset: 0x000726AC
		public virtual object Clone(object parent)
		{
			int a_ = 19;
			switch (0)
			{
			default:
				for (;;)
				{
					Type type = base.GetType();
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(spr\u2158),
						typeof(object)
					}, null);
					int num = 6;
					for (;;)
					{
						T t;
						CollectionExtended<T> collectionExtended;
						int num2;
						switch (num)
						{
						case 0:
							goto IL_BC;
						case 1:
							goto IL_1E9;
						case 2:
							if (t is ICloneParent)
							{
								num = 0;
								continue;
							}
							goto IL_134;
						case 3:
						{
							ICloneParent cloneParent = (ICloneParent)((object)t);
							t = (T)((object)cloneParent.Clone(collectionExtended));
							num = 8;
							continue;
						}
						case 4:
							goto IL_1E9;
						case 5:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BC;
							default:
								if (false)
								{
								}
								constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
								{
									typeof(spr\u1DF5),
									typeof(object)
								}, null);
								num = 11;
								continue;
							}
							break;
						case 6:
							if (constructor == null)
							{
								num = 5;
								continue;
							}
							goto IL_111;
						case 7:
							goto IL_12F;
						case 8:
							goto IL_134;
						case 9:
							return collectionExtended;
						case 10:
						{
							if (constructor == null)
							{
								num = 7;
								continue;
							}
							CollectionExtended<T> collectionExtended2 = constructor.Invoke(new object[]
							{
								this.ReservedHandle,
								parent
							}) as CollectionExtended<T>;
							collectionExtended = collectionExtended2;
							List<T> innerList = base.InnerList;
							num2 = 0;
							int count = innerList.Count;
							num = 4;
							continue;
						}
						case 11:
							goto IL_111;
						case 12:
							if (t is ICloneParent)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 13:
							goto IL_134;
						case 14:
						{
							int count;
							if (num2 >= count)
							{
								num = 9;
								continue;
							}
							List<T> innerList;
							t = innerList[num2];
							num = 12;
							continue;
						}
						}
						break;
						IL_BC:
						ICloneable cloneable = (ICloneable)((object)t);
						t = (T)((object)cloneable.Clone());
						num = 13;
						continue;
						IL_111:
						num = 10;
						continue;
						IL_134:
						collectionExtended.Add(t);
						num2++;
						num = 1;
						continue;
						IL_1E9:
						num = 14;
					}
				}
				IL_12F:
				throw new ApplicationException(RecordTableEnumerator.b("ੈ⩊⍌ⅎ㹐❒畔ㅖじ㕚㥜罞፠٢ᑤቦhᥪ࡬୮兰ၲᩴ᥶੸ེོ੾ꞈ", a_));
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0007395C File Offset: 0x0007295C
		public void EnsureCapacity(int size)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6E:
				num = 0;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_60;
				}
				if (true)
				{
				}
				if (base.InnerList.Capacity >= size)
				{
					return;
				}
				num = 1;
			}
			IL_60:
			base.InnerList.Capacity = size;
			goto IL_6E;
			IL_20:
			if (false)
			{
			}
			num = 2;
			goto IL_30;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000739E4 File Offset: 0x000729E4
		public static string GenerateDefaultName(ICollection<T> namesCollection, string strStart)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int val = 1;
				int length = strStart.Length;
				IEnumerator<T> enumerator = namesCollection.GetEnumerator();
				try
				{
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							string name;
							string s = name.Substring(length, name.Length - length);
							num = 4;
							continue;
						}
						case 1:
						{
							double num2;
							int val2 = (int)num2 + 1;
							val = Math.Max(val2, val);
							num = 2;
							continue;
						}
						case 3:
							num = 5;
							continue;
						case 4:
						{
							string s;
							double num2;
							if (double.TryParse(s, NumberStyles.Integer, null, out num2))
							{
								num = 1;
								continue;
							}
							break;
						}
						case 5:
							goto IL_141;
						case 6:
						{
							string name;
							if (name != null)
							{
								num = 8;
								continue;
							}
							break;
						}
						case 8:
							num = 10;
							continue;
						case 9:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							INamedObject namedObject = (INamedObject)((object)enumerator.Current);
							string name = namedObject.Name;
							num = 6;
							continue;
						}
						case 10:
						{
							string name;
							if (name.StartsWith(strStart))
							{
								num = 0;
								continue;
							}
							break;
						}
						}
						IL_86:
						num = 9;
						continue;
						goto IL_86;
					}
					IL_141:;
				}
				finally
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_193:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_18A;
						case 1:
							goto IL_19C;
						}
						if (enumerator == null)
						{
							goto IL_19E;
						}
						num = 0;
					}
					IL_18A:
					enumerator.Dispose();
					goto IL_193;
					IL_19C:
					IL_19E:;
				}
				return strStart + val.ToString();
			}
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00073BC4 File Offset: 0x00072BC4
		protected internal static string GenerateDefaultName(ICollection namesCollection, string strStart)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int val = 1;
				int length = strStart.Length;
				IEnumerator enumerator = namesCollection.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							string name;
							if (name.StartsWith(strStart))
							{
								num = 8;
								continue;
							}
							break;
						}
						case 3:
						{
							string s;
							double num2;
							if (double.TryParse(s, NumberStyles.Integer, null, out num2))
							{
								num = 9;
								continue;
							}
							break;
						}
						case 4:
						{
							string name;
							if (name != null)
							{
								num = 5;
								continue;
							}
							break;
						}
						case 5:
							num = 0;
							continue;
						case 6:
							num = 7;
							continue;
						case 7:
							goto IL_13C;
						case 8:
						{
							string name;
							string s = name.Substring(length, name.Length - length);
							num = 3;
							continue;
						}
						case 9:
						{
							double num2;
							int val2 = (int)num2 + 1;
							val = Math.Max(val2, val);
							num = 1;
							continue;
						}
						case 10:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							INamedObject namedObject = (INamedObject)enumerator.Current;
							string name = namedObject.Name;
							num = 4;
							continue;
						}
						}
						IL_86:
						num = 10;
						continue;
						goto IL_86;
					}
					IL_13C:;
				}
				finally
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
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1A0;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_1A2;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_1A0:
					IL_1A2:;
				}
				return strStart + val.ToString();
			}
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00073DA8 File Offset: 0x00072DA8
		protected internal static string GenerateDefaultName(string strStart, params ICollection[] arrCollections)
		{
			switch (0)
			{
			default:
			{
				int val;
				for (;;)
				{
					val = 1;
					int length = strStart.Length;
					int num = 0;
					int num2 = arrCollections.Length;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							try
							{
								num3 = 4;
								for (;;)
								{
									switch (num3)
									{
									case 0:
										goto IL_1A9;
									case 1:
									{
										double num4;
										int val2 = (int)num4 + 1;
										val = Math.Max(val2, val);
										num3 = 3;
										continue;
									}
									case 2:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num3 = 5;
											continue;
										}
										object obj = enumerator.Current;
										num3 = 12;
										continue;
									}
									case 5:
										num3 = 8;
										continue;
									case 6:
									{
										if (true)
										{
										}
										string text;
										string s = text.Substring(length, text.Length - length);
										num3 = 9;
										continue;
									}
									case 7:
										goto IL_1A9;
									case 8:
										goto IL_1FB;
									case 9:
									{
										double num4;
										string s;
										if (double.TryParse(s, NumberStyles.Integer, null, out num4))
										{
											num3 = 1;
											continue;
										}
										break;
									}
									case 10:
									{
										object obj;
										string text = (obj as INamedObject).Name;
										num3 = 0;
										continue;
									}
									case 11:
									{
										string text;
										if (text.StartsWith(strStart))
										{
											num3 = 6;
											continue;
										}
										break;
									}
									case 12:
									{
										object obj;
										if (obj is INamedObject)
										{
											num3 = 10;
											continue;
										}
										string text = obj.ToString();
										num3 = 7;
										continue;
									}
									}
									IL_11F:
									num3 = 2;
									continue;
									goto IL_11F;
									IL_1A9:
									num3 = 11;
								}
								IL_1FB:
								goto IL_48;
							}
							finally
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
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num3 = 0;
									for (;;)
									{
										switch (num3)
										{
										case 0:
											if (disposable != null)
											{
												num3 = 2;
												continue;
											}
											goto IL_264;
										case 1:
											goto IL_262;
										case 2:
											disposable.Dispose();
											num3 = 1;
											continue;
										}
										break;
									}
								}
								IL_262:
								IL_264:;
							}
							goto IL_265;
							IL_48:
							num++;
							num3 = 1;
							continue;
						case 1:
							goto IL_7A;
						case 2:
							goto IL_7A;
						case 3:
						{
							if (num >= num2)
							{
								num3 = 4;
								continue;
							}
							ICollection collection = arrCollections[num];
							IEnumerator enumerator = collection.GetEnumerator();
							num3 = 0;
							continue;
						}
						case 4:
							goto IL_96;
						}
						break;
						IL_7A:
						num3 = 3;
					}
				}
				IL_96:
				IL_265:
				return strStart + val.ToString();
			}
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00074044 File Offset: 0x00073044
		protected internal static void ChangeName(IDictionary hashNames, XlsEventArgs e)
		{
			int a_ = 6;
			string text;
			string text2;
			for (;;)
			{
				text = (string)e.oldValue;
				text2 = (string)e.newValue;
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D7;
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_BB;
					case 1:
						goto IL_80;
					case 2:
						if (!hashNames.Contains(text))
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						num = 3;
						continue;
					case 3:
						if (hashNames.Contains(text2))
						{
							num = 0;
							continue;
						}
						goto IL_D7;
					}
					break;
				}
			}
			IL_80:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("缻儽ⰿ⹁⅃╅㱇⍉⍋⁍灏㙑㭓㍕⭗穙㉛ㅝᑟ䉡ݣ॥٧ṩ൫ݭṯ剱᭳ᑵቷόύ੽ꁿ겋", a_) + text);
			IL_BB:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("缻儽ⰿ⹁⅃╅㱇⍉⍋⁍灏㍑㡓⑕㵗㭙㡛❝䁟šୣࡥᱧ୩իmͯ剱᭳ᑵቷόύ੽ꁿ겋", a_) + text2);
			IL_D7:
			object value = hashNames[text];
			hashNames.Remove(text);
			hashNames.Add(text2, value);
		}

		// Token: 0x040009ED RID: 2541
		private spr\u1DF5 ᜀ;

		// Token: 0x040009EE RID: 2542
		private object ᜁ;

		// Token: 0x040009EF RID: 2543
		private bool ᜂ;

		// Token: 0x040009F0 RID: 2544
		private EventHandler ᜃ;

		// Token: 0x040009F1 RID: 2545
		private CollectionExtended<T>.ᜂ ᜄ;

		// Token: 0x040009F2 RID: 2546
		private CollectionExtended<T>.ᜂ ᜅ;

		// Token: 0x040009F3 RID: 2547
		private CollectionExtended<T>.ᜀ ᜆ;

		// Token: 0x040009F4 RID: 2548
		private CollectionExtended<T>.ᜀ ᜇ;

		// Token: 0x040009F5 RID: 2549
		private CollectionExtended<T>.ᜀ ᜈ;

		// Token: 0x040009F6 RID: 2550
		private CollectionExtended<T>.ᜀ ᜉ;

		// Token: 0x040009F7 RID: 2551
		private CollectionExtended<T>.ᜁ ᜊ;

		// Token: 0x040009F8 RID: 2552
		private CollectionExtended<T>.ᜁ ᜋ;

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06001A6E RID: 6766
		internal delegate void ᜂ();

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06001A72 RID: 6770
		internal delegate void ᜀ(object A_0, CollectionChangeEventArgs<ᜀ> A_1);

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06001A76 RID: 6774
		internal delegate void ᜁ(int A_0, object A_1, object A_2);
	}
}
