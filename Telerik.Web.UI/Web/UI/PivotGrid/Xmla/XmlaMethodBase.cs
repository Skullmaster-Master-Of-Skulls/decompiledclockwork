using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D8E RID: 3470
	internal class XmlaMethodBase : IXmlaMethod
	{
		// Token: 0x0600810C RID: 33036 RVA: 0x001D7AA5 File Offset: 0x001D5CA5
		public XmlaMethodBase()
		{
			this.properties = new List<IXmlaMethodProperty>();
		}

		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x0600810D RID: 33037 RVA: 0x001D7AB8 File Offset: 0x001D5CB8
		public IEnumerable<IXmlaMethodProperty> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x001D7AC0 File Offset: 0x001D5CC0
		public void AddProperty(IXmlaMethodProperty property)
		{
			this.properties.Add(property);
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x001D7ACE File Offset: 0x001D5CCE
		public void RemoveProperty(IXmlaMethodProperty property)
		{
			this.properties.Remove(property);
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x001D7AE0 File Offset: 0x001D5CE0
		public void MergeProperties(Collection<XmlaQueryProperty> propertiesToMerge)
		{
			if (propertiesToMerge == null)
			{
				return;
			}
			foreach (IXmlaMethodProperty xmlaMethodProperty in propertiesToMerge)
			{
				this.RemoveExisting(xmlaMethodProperty.Name);
				this.properties.Add(xmlaMethodProperty);
			}
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x001D7B40 File Offset: 0x001D5D40
		public void MergeProperties(IEnumerable<IXmlaMethodProperty> propertiesToMerge)
		{
			if (propertiesToMerge == null)
			{
				return;
			}
			foreach (IXmlaMethodProperty xmlaMethodProperty in propertiesToMerge)
			{
				this.RemoveExisting(xmlaMethodProperty.Name);
				this.properties.Add(xmlaMethodProperty);
			}
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x001D7BBC File Offset: 0x001D5DBC
		private void RemoveExisting(string propertyName)
		{
			IXmlaMethodProperty xmlaMethodProperty = (from p in this.properties
			where p.Name == propertyName
			select p).FirstOrDefault<IXmlaMethodProperty>();
			if (xmlaMethodProperty != null)
			{
				this.properties.Remove(xmlaMethodProperty);
			}
		}

		// Token: 0x040023A1 RID: 9121
		private IList<IXmlaMethodProperty> properties;
	}
}
