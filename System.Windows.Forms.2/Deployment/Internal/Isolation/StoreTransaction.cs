using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000063 RID: 99
	internal class StoreTransaction : IDisposable
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00007CBE File Offset: 0x00005EBE
		public void Add(StoreOperationInstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007CD2 File Offset: 0x00005ED2
		public void Add(StoreOperationPinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007CE6 File Offset: 0x00005EE6
		public void Add(StoreOperationSetCanonicalizationContext o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007CFA File Offset: 0x00005EFA
		public void Add(StoreOperationSetDeploymentMetadata o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007D0E File Offset: 0x00005F0E
		public void Add(StoreOperationStageComponent o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007D22 File Offset: 0x00005F22
		public void Add(StoreOperationStageComponentFile o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007D36 File Offset: 0x00005F36
		public void Add(StoreOperationUninstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007D4A File Offset: 0x00005F4A
		public void Add(StoreOperationUnpinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007D5E File Offset: 0x00005F5E
		public void Add(StoreOperationScavenge o)
		{
			this._list.Add(o);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007D74 File Offset: 0x00005F74
		~StoreTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007DA4 File Offset: 0x00005FA4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007DB0 File Offset: 0x00005FB0
		[SecuritySafeCritical]
		private void Dispose(bool fDisposing)
		{
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
			StoreTransactionOperation[] storeOps = this._storeOps;
			this._storeOps = null;
			if (storeOps != null)
			{
				for (int num = 0; num != storeOps.Length; num++)
				{
					StoreTransactionOperation storeTransactionOperation = storeOps[num];
					if (storeTransactionOperation.Data.DataPtr != IntPtr.Zero)
					{
						switch (storeTransactionOperation.Operation)
						{
						case StoreTransactionOperationType.SetCanonicalizationContext:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetCanonicalizationContext));
							break;
						case StoreTransactionOperationType.StageComponent:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponent));
							break;
						case StoreTransactionOperationType.PinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationPinDeployment));
							break;
						case StoreTransactionOperationType.UnpinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUnpinDeployment));
							break;
						case StoreTransactionOperationType.StageComponentFile:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponentFile));
							break;
						case StoreTransactionOperationType.InstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationInstallDeployment));
							break;
						case StoreTransactionOperationType.UninstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUninstallDeployment));
							break;
						case StoreTransactionOperationType.SetDeploymentMetadata:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetDeploymentMetadata));
							break;
						case StoreTransactionOperationType.Scavenge:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationScavenge));
							break;
						}
						Marshal.FreeCoTaskMem(storeTransactionOperation.Data.DataPtr);
					}
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007F73 File Offset: 0x00006173
		public StoreTransactionOperation[] Operations
		{
			get
			{
				if (this._storeOps == null)
				{
					this._storeOps = this.GenerateStoreOpsList();
				}
				return this._storeOps;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007F90 File Offset: 0x00006190
		[SecuritySafeCritical]
		private StoreTransactionOperation[] GenerateStoreOpsList()
		{
			StoreTransactionOperation[] array = new StoreTransactionOperation[this._list.Count];
			for (int num = 0; num != this._list.Count; num++)
			{
				object obj = this._list[num];
				Type type = obj.GetType();
				array[num].Data.DataPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(obj));
				Marshal.StructureToPtr(obj, array[num].Data.DataPtr, false);
				if (type == typeof(StoreOperationSetCanonicalizationContext))
				{
					array[num].Operation = StoreTransactionOperationType.SetCanonicalizationContext;
				}
				else if (type == typeof(StoreOperationStageComponent))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponent;
				}
				else if (type == typeof(StoreOperationPinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.PinDeployment;
				}
				else if (type == typeof(StoreOperationUnpinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UnpinDeployment;
				}
				else if (type == typeof(StoreOperationStageComponentFile))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponentFile;
				}
				else if (type == typeof(StoreOperationInstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.InstallDeployment;
				}
				else if (type == typeof(StoreOperationUninstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UninstallDeployment;
				}
				else if (type == typeof(StoreOperationSetDeploymentMetadata))
				{
					array[num].Operation = StoreTransactionOperationType.SetDeploymentMetadata;
				}
				else
				{
					if (!(type == typeof(StoreOperationScavenge)))
					{
						throw new Exception("How did you get here?");
					}
					array[num].Operation = StoreTransactionOperationType.Scavenge;
				}
			}
			return array;
		}

		// Token: 0x040001A7 RID: 423
		private ArrayList _list = new ArrayList();

		// Token: 0x040001A8 RID: 424
		private StoreTransactionOperation[] _storeOps;
	}
}
