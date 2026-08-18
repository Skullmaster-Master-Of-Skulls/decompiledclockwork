using System;
using System.Globalization;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000D RID: 13
	public sealed class ApplicationPoolDefaults : ConfigurationElement
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003D6C File Offset: 0x00002D6C
		internal ApplicationPoolDefaults()
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D74 File Offset: 0x00002D74
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003D86 File Offset: 0x00002D86
		public bool AutoStart
		{
			get
			{
				return (bool)base["autoStart"];
			}
			set
			{
				base["autoStart"] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003D9C File Offset: 0x00002D9C
		public ApplicationPoolCpu Cpu
		{
			get
			{
				if (this._cpu == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("cpu");
					this._cpu = new ApplicationPoolCpu();
					this._cpu.Initialize(base.Configuration, elementByName);
				}
				return this._cpu;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003DE5 File Offset: 0x00002DE5
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003DF7 File Offset: 0x00002DF7
		public bool Enable32BitAppOnWin64
		{
			get
			{
				return (bool)base["enable32BitAppOnWin64"];
			}
			set
			{
				base["enable32BitAppOnWin64"] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003E0C File Offset: 0x00002E0C
		public ApplicationPoolFailure Failure
		{
			get
			{
				if (this._failure == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("failure");
					this._failure = new ApplicationPoolFailure();
					this._failure.Initialize(base.Configuration, elementByName);
				}
				return this._failure;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003E55 File Offset: 0x00002E55
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003E67 File Offset: 0x00002E67
		public ManagedPipelineMode ManagedPipelineMode
		{
			get
			{
				return (ManagedPipelineMode)base["managedPipelineMode"];
			}
			set
			{
				base["managedPipelineMode"] = (int)value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003E7A File Offset: 0x00002E7A
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003E8C File Offset: 0x00002E8C
		public string ManagedRuntimeVersion
		{
			get
			{
				return (string)base["managedRuntimeVersion"];
			}
			set
			{
				base["managedRuntimeVersion"] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003E9C File Offset: 0x00002E9C
		public ApplicationPoolProcessModel ProcessModel
		{
			get
			{
				if (this._processModel == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("processModel");
					this._processModel = new ApplicationPoolProcessModel();
					this._processModel.Initialize(base.Configuration, elementByName);
				}
				return this._processModel;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003EE5 File Offset: 0x00002EE5
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003EF8 File Offset: 0x00002EF8
		public long QueueLength
		{
			get
			{
				return (long)base["queueLength"];
			}
			set
			{
				if (value < 10L || value > 65535L)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"QueueLength",
						10,
						65535
					}));
				}
				base["queueLength"] = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003F68 File Offset: 0x00002F68
		public ApplicationPoolRecycling Recycling
		{
			get
			{
				if (this._recycling == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("recycling");
					this._recycling = new ApplicationPoolRecycling();
					this._recycling.Initialize(base.Configuration, elementByName);
				}
				return this._recycling;
			}
		}

		// Token: 0x04000024 RID: 36
		private ApplicationPoolCpu _cpu;

		// Token: 0x04000025 RID: 37
		private ApplicationPoolFailure _failure;

		// Token: 0x04000026 RID: 38
		private ApplicationPoolProcessModel _processModel;

		// Token: 0x04000027 RID: 39
		private ApplicationPoolRecycling _recycling;
	}
}
