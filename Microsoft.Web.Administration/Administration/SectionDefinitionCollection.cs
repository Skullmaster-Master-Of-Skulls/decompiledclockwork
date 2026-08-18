using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006B RID: 107
	[DebuggerDisplay("Count = {Count}")]
	public sealed class SectionDefinitionCollection : ICollection, IEnumerable<SectionDefinition>, IEnumerable
	{
		// Token: 0x060002CD RID: 717 RVA: 0x000077D6 File Offset: 0x000067D6
		internal SectionDefinitionCollection(SectionGroup parentSectionGroup, IAppHostSectionDefinitionCollection sectionDefinitions)
		{
			this._parentSectionGroup = parentSectionGroup;
			this._sectionDefinitions = sectionDefinitions;
			this._sections = new List<SectionDefinition>((int)(this._sectionDefinitions.Count + 1U));
			this.AddSections(this._sectionDefinitions);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00007810 File Offset: 0x00006810
		public int Count
		{
			get
			{
				return this._sections.Count;
			}
		}

		// Token: 0x17000165 RID: 357
		public SectionDefinition this[string sectionName]
		{
			get
			{
				foreach (SectionDefinition sectionDefinition in this._sections)
				{
					if (string.Equals(sectionDefinition.Name, sectionName, StringComparison.OrdinalIgnoreCase))
					{
						return sectionDefinition;
					}
				}
				return null;
			}
		}

		// Token: 0x17000166 RID: 358
		public SectionDefinition this[int index]
		{
			get
			{
				return this._sections[index];
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007894 File Offset: 0x00006894
		public SectionDefinition Add(string sectionName)
		{
			this._parentSectionGroup.SetDirty();
			IAppHostSectionDefinition sectionDefinition = this._sectionDefinitions.AddSection(sectionName);
			SectionDefinition sectionDefinition2 = new SectionDefinition(this._parentSectionGroup, sectionDefinition);
			this._sections.Add(sectionDefinition2);
			return sectionDefinition2;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000078D3 File Offset: 0x000068D3
		public IEnumerator<SectionDefinition> GetEnumerator()
		{
			return this._sections.GetEnumerator();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000078E8 File Offset: 0x000068E8
		internal void AddSections(IAppHostSectionDefinitionCollection sectionDefinitions)
		{
			int num = 0;
			while ((long)num < (long)((ulong)sectionDefinitions.Count))
			{
				IAppHostSectionDefinition sectionDefinition = sectionDefinitions[num];
				this._sections.Add(new SectionDefinition(this._parentSectionGroup, sectionDefinition));
				num++;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000792C File Offset: 0x0000692C
		public void Remove(string sectionName)
		{
			SectionDefinition sectionDefinition = this[sectionName];
			if (sectionDefinition != null)
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinitions.DeleteSection(sectionDefinition.Name);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00007960 File Offset: 0x00006960
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._sections).IsSynchronized;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000796D File Offset: 0x0000696D
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._sections).SyncRoot;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000797A File Offset: 0x0000697A
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._sections).CopyTo(array, index);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007989 File Offset: 0x00006989
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._sections.GetEnumerator();
		}

		// Token: 0x04000105 RID: 261
		private IAppHostSectionDefinitionCollection _sectionDefinitions;

		// Token: 0x04000106 RID: 262
		private List<SectionDefinition> _sections;

		// Token: 0x04000107 RID: 263
		private SectionGroup _parentSectionGroup;
	}
}
