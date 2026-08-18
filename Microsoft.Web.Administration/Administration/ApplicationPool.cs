using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200000A RID: 10
	[DebuggerDisplay("Name = {Name}")]
	public sealed class ApplicationPool : ConfigurationElement
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003635 File Offset: 0x00002635
		internal ApplicationPool(ServerManager owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003644 File Offset: 0x00002644
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003656 File Offset: 0x00002656
		public bool AutoStart
		{
			get
			{
				return (bool)base.GetAttributeValue("autoStart");
			}
			set
			{
				base["autoStart"] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000366C File Offset: 0x0000266C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000036B5 File Offset: 0x000026B5
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000036C7 File Offset: 0x000026C7
		public bool Enable32BitAppOnWin64
		{
			get
			{
				return (bool)base.GetAttributeValue("enable32BitAppOnWin64");
			}
			set
			{
				base["enable32BitAppOnWin64"] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000036DC File Offset: 0x000026DC
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003725 File Offset: 0x00002725
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003737 File Offset: 0x00002737
		public ManagedPipelineMode ManagedPipelineMode
		{
			get
			{
				return (ManagedPipelineMode)base.GetAttributeValue("managedPipelineMode");
			}
			set
			{
				base["managedPipelineMode"] = (int)value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000374A File Offset: 0x0000274A
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000375C File Offset: 0x0000275C
		public string ManagedRuntimeVersion
		{
			get
			{
				return (string)base.GetAttributeValue("managedRuntimeVersion");
			}
			set
			{
				base["managedRuntimeVersion"] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000376A File Offset: 0x0000276A
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000377C File Offset: 0x0000277C
		public string Name
		{
			get
			{
				return (string)this.NameProperty.Value;
			}
			set
			{
				this.NameProperty.Value = value;
				base.SetDirty();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003790 File Offset: 0x00002790
		private IAppHostProperty NameProperty
		{
			get
			{
				if (this._nameProperty == null)
				{
					this._nameProperty = base.AppHostElement.GetPropertyByName("name");
				}
				return this._nameProperty;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000037B8 File Offset: 0x000027B8
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003801 File Offset: 0x00002801
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003814 File Offset: 0x00002814
		public long QueueLength
		{
			get
			{
				return (long)base.GetAttributeValue("queueLength");
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003884 File Offset: 0x00002884
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000038D0 File Offset: 0x000028D0
		public ObjectState State
		{
			get
			{
				ObjectState result = ObjectState.Unknown;
				try
				{
					result = (ObjectState)base["state"];
				}
				catch
				{
					if (this._owner.ServerName != null || ServerManager.GetServiceStatus("WAS") == ServiceControllerStatus.Running)
					{
						throw;
					}
					result = ObjectState.Stopped;
				}
				return result;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003928 File Offset: 0x00002928
		public WorkerProcessCollection WorkerProcesses
		{
			get
			{
				if (this._workerProcesses == null)
				{
					this._workerProcesses = (WorkerProcessCollection)base.GetCollection("workerProcesses", typeof(WorkerProcessCollection));
				}
				return this._workerProcesses;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003958 File Offset: 0x00002958
		public ObjectState Recycle()
		{
			ObjectState state;
			try
			{
				base.ExecuteMethod("Recycle");
				state = this.State;
			}
			catch (Exception)
			{
				if (this._owner.ServerName == null && ServerManager.GetServiceStatus("WAS") != ServiceControllerStatus.Running)
				{
					throw new ServerManagerException(Resources.UnableToStartAppPoolWasNotStarted, 100);
				}
				throw;
			}
			return state;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039B4 File Offset: 0x000029B4
		public ObjectState Start()
		{
			ObjectState state;
			try
			{
				base.ExecuteMethod("Start");
				state = this.State;
			}
			catch
			{
				if (this._owner.ServerName == null && ServerManager.GetServiceStatus("WAS") != ServiceControllerStatus.Running)
				{
					throw new ServerManagerException(Resources.UnableToStartAppPoolWasNotStarted, 100);
				}
				throw;
			}
			return state;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A10 File Offset: 0x00002A10
		public ObjectState Stop()
		{
			ObjectState result;
			try
			{
				base.ExecuteMethod("Stop");
				result = this.State;
			}
			catch
			{
				if (this._owner.ServerName != null)
				{
					throw;
				}
				if (ServerManager.GetServiceStatus("WAS") == ServiceControllerStatus.Running)
				{
					throw;
				}
				result = ObjectState.Unknown;
			}
			return result;
		}

		// Token: 0x0400001C RID: 28
		private ApplicationPoolCpu _cpu;

		// Token: 0x0400001D RID: 29
		private ApplicationPoolFailure _failure;

		// Token: 0x0400001E RID: 30
		private ApplicationPoolProcessModel _processModel;

		// Token: 0x0400001F RID: 31
		private ApplicationPoolRecycling _recycling;

		// Token: 0x04000020 RID: 32
		private WorkerProcessCollection _workerProcesses;

		// Token: 0x04000021 RID: 33
		private IAppHostProperty _nameProperty;

		// Token: 0x04000022 RID: 34
		private ServerManager _owner;
	}
}
