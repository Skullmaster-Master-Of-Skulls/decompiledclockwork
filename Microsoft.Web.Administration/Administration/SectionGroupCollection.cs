using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000069 RID: 105
	[DebuggerDisplay("Count = {Count}")]
	public sealed class SectionGroupCollection : ICollection, IEnumerable<SectionGroup>, IEnumerable
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00007504 File Offset: 0x00006504
		internal SectionGroupCollection(SectionGroup parentSectionGroup, IAppHostSectionGroup nativeSectionGroup)
		{
			this._parentSectionGroup = parentSectionGroup;
			this._nativeSectionGroup = nativeSectionGroup;
			this.Initialize();
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00007520 File Offset: 0x00006520
		public int Count
		{
			get
			{
				return this._sectionGroups.Count;
			}
		}

		// Token: 0x1700015A RID: 346
		public SectionGroup this[string sectionGroupName]
		{
			get
			{
				foreach (SectionGroup sectionGroup in this._sectionGroups)
				{
					if (string.Equals(sectionGroupName, sectionGroup.Name, StringComparison.OrdinalIgnoreCase))
					{
						return sectionGroup;
					}
				}
				return null;
			}
		}

		// Token: 0x1700015B RID: 347
		public SectionGroup this[int index]
		{
			get
			{
				return this._sectionGroups[index];
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000075A4 File Offset: 0x000065A4
		public SectionGroup Add(string sectionGroupName)
		{
			this._parentSectionGroup.SetDirty();
			IAppHostSectionGroup sectionGroup = this._nativeSectionGroup.AddSectionGroup(sectionGroupName);
			SectionGroup sectionGroup2 = new SectionGroup(this._parentSectionGroup.Configuration, sectionGroup);
			this._sectionGroups.Add(sectionGroup2);
			return sectionGroup2;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000075E8 File Offset: 0x000065E8
		public IEnumerator<SectionGroup> GetEnumerator()
		{
			return this._sectionGroups.GetEnumerator();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000075FC File Offset: 0x000065FC
		private void Initialize()
		{
			this._sectionGroups = new List<SectionGroup>((int)(this._nativeSectionGroup.Count + 1U));
			int num = 0;
			while ((long)num < (long)((ulong)this._nativeSectionGroup.Count))
			{
				IAppHostSectionGroup sectionGroup = this._nativeSectionGroup[num];
				this._sectionGroups.Add(new SectionGroup(this._parentSectionGroup.Configuration, sectionGroup));
				num++;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007668 File Offset: 0x00006668
		public void Remove(string sectionGroupName)
		{
			SectionGroup sectionGroup = this[sectionGroupName];
			if (sectionGroup != null)
			{
				this._parentSectionGroup.SetDirty();
				this._nativeSectionGroup.DeleteSectionGroup(sectionGroup.Name);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000769C File Offset: 0x0000669C
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._sectionGroups).IsSynchronized;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000076A9 File Offset: 0x000066A9
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._sectionGroups).SyncRoot;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000076B6 File Offset: 0x000066B6
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._sectionGroups).CopyTo(array, index);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000076C5 File Offset: 0x000066C5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._sectionGroups.GetEnumerator();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000076D7 File Offset: 0x000066D7
		internal void AddInternal(IAppHostSectionGroup childSectionGroup)
		{
			this._sectionGroups.Add(new SectionGroup(this._parentSectionGroup.Configuration, childSectionGroup));
		}

		// Token: 0x04000100 RID: 256
		private SectionGroup _parentSectionGroup;

		// Token: 0x04000101 RID: 257
		private List<SectionGroup> _sectionGroups;

		// Token: 0x04000102 RID: 258
		private IAppHostSectionGroup _nativeSectionGroup;
	}
}
