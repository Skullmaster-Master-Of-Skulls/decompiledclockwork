using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000043 RID: 67
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x0600025B RID: 603 RVA: 0x001DEE58 File Offset: 0x001DE258
		static SmiMetaDataPropertyCollection()
		{
			SmiMetaDataPropertyCollection.EmptyInstance = new SmiMetaDataPropertyCollection();
			SmiMetaDataPropertyCollection.EmptyInstance.SetReadOnly();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x001DEEA8 File Offset: 0x001DE2A8
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.__emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.__emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.__emptyUniqueKey;
		}

		// Token: 0x17000041 RID: 65
		internal SmiMetaDataProperty this[SmiPropertySelector key]
		{
			get
			{
				return this._properties[(int)key];
			}
			set
			{
				if (value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
				}
				this.EnsureWritable();
				this._properties[(int)key] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600025F RID: 607 RVA: 0x001DEF48 File Offset: 0x001DE348
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000260 RID: 608 RVA: 0x001DEF68 File Offset: 0x001DE368
		internal IEnumerable<SmiMetaDataProperty> Values
		{
			get
			{
				return new List<SmiMetaDataProperty>(this._properties);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x001DEF88 File Offset: 0x001DE388
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x001DEFA8 File Offset: 0x001DE3A8
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x040005F2 RID: 1522
		private const int SelectorCount = 3;

		// Token: 0x040005F3 RID: 1523
		private SmiMetaDataProperty[] _properties;

		// Token: 0x040005F4 RID: 1524
		private bool _isReadOnly;

		// Token: 0x040005F5 RID: 1525
		internal static readonly SmiMetaDataPropertyCollection EmptyInstance;

		// Token: 0x040005F6 RID: 1526
		private static readonly SmiDefaultFieldsProperty __emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x040005F7 RID: 1527
		private static readonly SmiOrderProperty __emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x040005F8 RID: 1528
		private static readonly SmiUniqueKeyProperty __emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());
	}
}
