using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000070 RID: 112
	internal sealed class FileMonitor
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0000A964 File Offset: 0x00008B64
		internal FileMonitor(DirectoryMonitor dirMon, string fileNameLong, string fileNameShort, bool exists, FileAttributesData fad, byte[] dacl)
		{
			this.DirectoryMonitor = dirMon;
			this._fileNameLong = fileNameLong;
			this._fileNameShort = fileNameShort;
			this._exists = exists;
			this._fad = fad;
			this._dacl = dacl;
			this._targets = new HybridDictionary();
			this.Aliases = new HybridDictionary(true);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000A9BB File Offset: 0x00008BBB
		internal string FileNameLong
		{
			get
			{
				return this._fileNameLong;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000A9C3 File Offset: 0x00008BC3
		internal string FileNameShort
		{
			get
			{
				return this._fileNameShort;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000A9CB File Offset: 0x00008BCB
		internal bool Exists
		{
			get
			{
				return this._exists;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		internal bool IsDirectory
		{
			get
			{
				return this.FileNameLong == null;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000A9DE File Offset: 0x00008BDE
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000A9E6 File Offset: 0x00008BE6
		internal FileAction LastAction
		{
			get
			{
				return this._lastAction;
			}
			set
			{
				this._lastAction = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000A9EF File Offset: 0x00008BEF
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000A9F7 File Offset: 0x00008BF7
		internal DateTime UtcLastCompletion
		{
			get
			{
				return this._utcLastCompletion;
			}
			set
			{
				this._utcLastCompletion = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000AA00 File Offset: 0x00008C00
		internal FileAttributesData Attributes
		{
			get
			{
				return this._fad;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000AA08 File Offset: 0x00008C08
		internal byte[] Dacl
		{
			get
			{
				return this._dacl;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000AA10 File Offset: 0x00008C10
		internal void ResetCachedAttributes()
		{
			this._fad = null;
			this._dacl = null;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000AA20 File Offset: 0x00008C20
		internal void UpdateCachedAttributes()
		{
			string text = Path.Combine(this.DirectoryMonitor.Directory, this.FileNameLong);
			FileAttributesData.GetFileAttributes(text, out this._fad);
			this._dacl = FileSecurity.GetDacl(text);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000AA5D File Offset: 0x00008C5D
		internal void MakeExist(FindFileData ffd, byte[] dacl)
		{
			this._fileNameLong = ffd.FileNameLong;
			this._fileNameShort = ffd.FileNameShort;
			this._fad = ffd.FileAttributesData;
			this._dacl = dacl;
			this._exists = true;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000AA91 File Offset: 0x00008C91
		internal void MakeExtinct()
		{
			this._fad = null;
			this._dacl = null;
			this._exists = false;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		internal void RemoveFileNameShort()
		{
			this._fileNameShort = null;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000AAB1 File Offset: 0x00008CB1
		internal ICollection Targets
		{
			get
			{
				return this._targets.Values;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		internal void AddTarget(FileChangeEventHandler callback, string alias, bool newAlias)
		{
			FileMonitorTarget fileMonitorTarget = (FileMonitorTarget)this._targets[callback.Target];
			if (fileMonitorTarget != null)
			{
				fileMonitorTarget.AddRef();
			}
			else
			{
				this._targets.Add(callback.Target, new FileMonitorTarget(callback, alias));
			}
			if (newAlias)
			{
				this.Aliases[alias] = alias;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000AB18 File Offset: 0x00008D18
		internal int RemoveTarget(object callbackTarget)
		{
			FileMonitorTarget fileMonitorTarget = (FileMonitorTarget)this._targets[callbackTarget];
			if (fileMonitorTarget != null && fileMonitorTarget.Release() == 0)
			{
				this._targets.Remove(callbackTarget);
			}
			return this._targets.Count;
		}

		// Token: 0x04000202 RID: 514
		internal readonly DirectoryMonitor DirectoryMonitor;

		// Token: 0x04000203 RID: 515
		internal readonly HybridDictionary Aliases;

		// Token: 0x04000204 RID: 516
		private string _fileNameLong;

		// Token: 0x04000205 RID: 517
		private string _fileNameShort;

		// Token: 0x04000206 RID: 518
		private HybridDictionary _targets;

		// Token: 0x04000207 RID: 519
		private bool _exists;

		// Token: 0x04000208 RID: 520
		private FileAttributesData _fad;

		// Token: 0x04000209 RID: 521
		private byte[] _dacl;

		// Token: 0x0400020A RID: 522
		private FileAction _lastAction;

		// Token: 0x0400020B RID: 523
		private DateTime _utcLastCompletion;
	}
}
