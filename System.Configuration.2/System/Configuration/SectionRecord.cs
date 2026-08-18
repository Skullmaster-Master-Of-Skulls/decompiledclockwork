using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x02000089 RID: 137
	[DebuggerDisplay("SectionRecord {ConfigKey}")]
	internal class SectionRecord
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x0001B6B9 File Offset: 0x000198B9
		internal SectionRecord(string configKey)
		{
			this._configKey = configKey;
			this._result = SectionRecord.s_unevaluated;
			this._resultRuntimeObject = SectionRecord.s_unevaluated;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001B6DE File Offset: 0x000198DE
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001B6E6 File Offset: 0x000198E6
		internal bool Locked
		{
			get
			{
				return this._flags[1];
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001B6F4 File Offset: 0x000198F4
		internal bool LockChildren
		{
			get
			{
				return this._flags[2];
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001B704 File Offset: 0x00019904
		internal bool LockChildrenWithoutFileInput
		{
			get
			{
				bool result = this.LockChildren;
				if (this.HasFileInput)
				{
					result = this._flags[64];
				}
				return result;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001B72F File Offset: 0x0001992F
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0001B73D File Offset: 0x0001993D
		internal bool IsResultTrustedWithoutAptca
		{
			get
			{
				return this._flags[4];
			}
			set
			{
				this._flags[4] = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001B74C File Offset: 0x0001994C
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001B75A File Offset: 0x0001995A
		internal bool RequirePermission
		{
			get
			{
				return this._flags[8];
			}
			set
			{
				this._flags[8] = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001B769 File Offset: 0x00019969
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0001B77B File Offset: 0x0001997B
		internal bool AddUpdate
		{
			get
			{
				return this._flags[65536];
			}
			set
			{
				this._flags[65536] = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001B78E File Offset: 0x0001998E
		internal bool HasLocationInputs
		{
			get
			{
				return this._locationInputs != null && this._locationInputs.Count > 0;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001B7A8 File Offset: 0x000199A8
		internal List<SectionInput> LocationInputs
		{
			get
			{
				return this._locationInputs;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001B7B0 File Offset: 0x000199B0
		internal SectionInput LastLocationInput
		{
			get
			{
				if (this.HasLocationInputs)
				{
					return this._locationInputs[this._locationInputs.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001B7D4 File Offset: 0x000199D4
		internal void AddLocationInput(SectionInput sectionInput)
		{
			this.AddLocationInputImpl(sectionInput, false);
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001B7DE File Offset: 0x000199DE
		internal bool HasFileInput
		{
			get
			{
				return this._fileInput != null;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001B7E9 File Offset: 0x000199E9
		internal SectionInput FileInput
		{
			get
			{
				return this._fileInput;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001B7F1 File Offset: 0x000199F1
		internal void ChangeLockSettings(OverrideMode forSelf, OverrideMode forChildren)
		{
			if (forSelf != OverrideMode.Inherit)
			{
				this._flags[1] = (forSelf == OverrideMode.Deny);
				this._flags[2] = (forSelf == OverrideMode.Deny);
			}
			if (forChildren != OverrideMode.Inherit)
			{
				this._flags[2] = (forSelf == OverrideMode.Deny || forChildren == OverrideMode.Deny);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001B830 File Offset: 0x00019A30
		internal void AddFileInput(SectionInput sectionInput)
		{
			this._fileInput = sectionInput;
			if (!sectionInput.HasErrors && sectionInput.SectionXmlInfo.OverrideModeSetting.OverrideMode != OverrideMode.Inherit)
			{
				this._flags[64] = this.LockChildren;
				this.ChangeLockSettings(OverrideMode.Inherit, sectionInput.SectionXmlInfo.OverrideModeSetting.OverrideMode);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001B88E File Offset: 0x00019A8E
		internal void RemoveFileInput()
		{
			if (this._fileInput != null)
			{
				this._fileInput = null;
				this._flags[2] = this.Locked;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001B8B1 File Offset: 0x00019AB1
		internal bool HasIndirectLocationInputs
		{
			get
			{
				return this._indirectLocationInputs != null && this._indirectLocationInputs.Count > 0;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001B8CB File Offset: 0x00019ACB
		internal List<SectionInput> IndirectLocationInputs
		{
			get
			{
				return this._indirectLocationInputs;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001B8D3 File Offset: 0x00019AD3
		internal SectionInput LastIndirectLocationInput
		{
			get
			{
				if (this.HasIndirectLocationInputs)
				{
					return this._indirectLocationInputs[this._indirectLocationInputs.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		internal void AddIndirectLocationInput(SectionInput sectionInput)
		{
			this.AddLocationInputImpl(sectionInput, true);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001B904 File Offset: 0x00019B04
		private void AddLocationInputImpl(SectionInput sectionInput, bool isIndirectLocation)
		{
			List<SectionInput> list = isIndirectLocation ? this._indirectLocationInputs : this._locationInputs;
			int bit = isIndirectLocation ? 32 : 16;
			if (list == null)
			{
				list = new List<SectionInput>(1);
				if (isIndirectLocation)
				{
					this._indirectLocationInputs = list;
				}
				else
				{
					this._locationInputs = list;
				}
			}
			list.Insert(0, sectionInput);
			if (!sectionInput.HasErrors && !this._flags[bit])
			{
				OverrideMode overrideMode = sectionInput.SectionXmlInfo.OverrideModeSetting.OverrideMode;
				if (overrideMode != OverrideMode.Inherit)
				{
					this.ChangeLockSettings(overrideMode, overrideMode);
					this._flags[bit] = true;
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001B994 File Offset: 0x00019B94
		internal bool HasInput
		{
			get
			{
				return this.HasLocationInputs || this.HasFileInput || this.HasIndirectLocationInputs;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		internal void ClearRawXml()
		{
			if (this.HasLocationInputs)
			{
				foreach (SectionInput sectionInput in this.LocationInputs)
				{
					sectionInput.SectionXmlInfo.RawXml = null;
				}
			}
			if (this.HasIndirectLocationInputs)
			{
				foreach (SectionInput sectionInput2 in this.IndirectLocationInputs)
				{
					sectionInput2.SectionXmlInfo.RawXml = null;
				}
			}
			if (this.HasFileInput)
			{
				this.FileInput.SectionXmlInfo.RawXml = null;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001BA78 File Offset: 0x00019C78
		internal bool HasResult
		{
			get
			{
				return this._result != SectionRecord.s_unevaluated;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001BA8A File Offset: 0x00019C8A
		internal bool HasResultRuntimeObject
		{
			get
			{
				return this._resultRuntimeObject != SectionRecord.s_unevaluated;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001BA9C File Offset: 0x00019C9C
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		internal object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001BAAD File Offset: 0x00019CAD
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		internal object ResultRuntimeObject
		{
			get
			{
				return this._resultRuntimeObject;
			}
			set
			{
				this._resultRuntimeObject = value;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		internal void ClearResult()
		{
			if (this._fileInput != null)
			{
				this._fileInput.ClearResult();
			}
			if (this._locationInputs != null)
			{
				foreach (SectionInput sectionInput in this._locationInputs)
				{
					sectionInput.ClearResult();
				}
			}
			this._result = SectionRecord.s_unevaluated;
			this._resultRuntimeObject = SectionRecord.s_unevaluated;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001BB44 File Offset: 0x00019D44
		private List<ConfigurationException> GetAllErrors()
		{
			List<ConfigurationException> result = null;
			if (this.HasLocationInputs)
			{
				foreach (SectionInput sectionInput in this.LocationInputs)
				{
					ErrorsHelper.AddErrors(ref result, sectionInput.Errors);
				}
			}
			if (this.HasIndirectLocationInputs)
			{
				foreach (SectionInput sectionInput2 in this.IndirectLocationInputs)
				{
					ErrorsHelper.AddErrors(ref result, sectionInput2.Errors);
				}
			}
			if (this.HasFileInput)
			{
				ErrorsHelper.AddErrors(ref result, this.FileInput.Errors);
			}
			return result;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001BC14 File Offset: 0x00019E14
		internal bool HasErrors
		{
			get
			{
				if (this.HasLocationInputs)
				{
					foreach (SectionInput sectionInput in this.LocationInputs)
					{
						if (sectionInput.HasErrors)
						{
							return true;
						}
					}
				}
				if (this.HasIndirectLocationInputs)
				{
					foreach (SectionInput sectionInput2 in this.IndirectLocationInputs)
					{
						if (sectionInput2.HasErrors)
						{
							return true;
						}
					}
				}
				return this.HasFileInput && this.FileInput.HasErrors;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		internal void ThrowOnErrors()
		{
			if (this.HasErrors)
			{
				throw new ConfigurationErrorsException(this.GetAllErrors());
			}
		}

		// Token: 0x0400031D RID: 797
		private const int Flag_Locked = 1;

		// Token: 0x0400031E RID: 798
		private const int Flag_LockChildren = 2;

		// Token: 0x0400031F RID: 799
		private const int Flag_IsResultTrustedWithoutAptca = 4;

		// Token: 0x04000320 RID: 800
		private const int Flag_RequirePermission = 8;

		// Token: 0x04000321 RID: 801
		private const int Flag_LocationInputLockApplied = 16;

		// Token: 0x04000322 RID: 802
		private const int Flag_IndirectLocationInputLockApplied = 32;

		// Token: 0x04000323 RID: 803
		private const int Flag_ChildrenLockWithoutFileInput = 64;

		// Token: 0x04000324 RID: 804
		private const int Flag_AddUpdate = 65536;

		// Token: 0x04000325 RID: 805
		private static object s_unevaluated = new object();

		// Token: 0x04000326 RID: 806
		private SafeBitVector32 _flags;

		// Token: 0x04000327 RID: 807
		private string _configKey;

		// Token: 0x04000328 RID: 808
		private List<SectionInput> _locationInputs;

		// Token: 0x04000329 RID: 809
		private SectionInput _fileInput;

		// Token: 0x0400032A RID: 810
		private List<SectionInput> _indirectLocationInputs;

		// Token: 0x0400032B RID: 811
		private object _result;

		// Token: 0x0400032C RID: 812
		private object _resultRuntimeObject;
	}
}
