using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076F RID: 1903
	internal class ClonedPropertyValues : InternalPropertyValues
	{
		// Token: 0x06005654 RID: 22100 RVA: 0x001762AC File Offset: 0x001744AC
		internal ClonedPropertyValues(InternalPropertyValues original, DbDataRecord valuesRecord = null) : base(original.InternalContext, original.ObjectType, original.IsEntityValues)
		{
			this._propertyNames = original.PropertyNames;
			this._propertyValues = new Dictionary<string, ClonedPropertyValuesItem>(this._propertyNames.Count);
			foreach (string text in this._propertyNames)
			{
				IPropertyValuesItem item = original.GetItem(text);
				object obj = item.Value;
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					DbDataRecord valuesRecord2 = (valuesRecord == null) ? null : ((DbDataRecord)valuesRecord[text]);
					obj = new ClonedPropertyValues(internalPropertyValues, valuesRecord2);
				}
				else if (valuesRecord != null)
				{
					obj = valuesRecord[text];
					if (obj == DBNull.Value)
					{
						obj = null;
					}
				}
				this._propertyValues[text] = new ClonedPropertyValuesItem(text, obj, item.Type, item.IsComplex);
			}
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x001763A0 File Offset: 0x001745A0
		protected override IPropertyValuesItem GetItemImpl(string propertyName)
		{
			return this._propertyValues[propertyName];
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x001763AE File Offset: 0x001745AE
		public override ISet<string> PropertyNames
		{
			get
			{
				return this._propertyNames;
			}
		}

		// Token: 0x040022F5 RID: 8949
		private readonly ISet<string> _propertyNames;

		// Token: 0x040022F6 RID: 8950
		private readonly IDictionary<string, ClonedPropertyValuesItem> _propertyValues;
	}
}
