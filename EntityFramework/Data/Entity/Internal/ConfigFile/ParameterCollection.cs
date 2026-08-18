using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006BA RID: 1722
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class ParameterCollection : ConfigurationElementCollection
	{
		// Token: 0x0600448A RID: 17546 RVA: 0x0014440C File Offset: 0x0014260C
		protected override ConfigurationElement CreateNewElement()
		{
			ParameterElement result = new ParameterElement(this._nextKey);
			this._nextKey++;
			return result;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00144434 File Offset: 0x00142634
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ParameterElement)element).Key;
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x00144446 File Offset: 0x00142646
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x00144449 File Offset: 0x00142649
		protected override string ElementName
		{
			get
			{
				return "parameter";
			}
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x00144458 File Offset: 0x00142658
		public virtual object[] GetTypedParameterValues()
		{
			return (from ParameterElement e in this
			select e.GetTypedParameterValue()).ToArray<object>();
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x00144488 File Offset: 0x00142688
		internal ParameterElement NewElement()
		{
			ConfigurationElement configurationElement = this.CreateNewElement();
			base.BaseAdd(configurationElement);
			return (ParameterElement)configurationElement;
		}

		// Token: 0x0400193F RID: 6463
		private const string ParameterKey = "parameter";

		// Token: 0x04001940 RID: 6464
		private int _nextKey;
	}
}
