using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000078 RID: 120
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x00047FB8 File Offset: 0x000473B8
		static SmiMetaDataPropertyCollection()
		{
			SmiMetaDataPropertyCollection.EmptyInstance = new SmiMetaDataPropertyCollection();
			SmiMetaDataPropertyCollection.EmptyInstance.SetReadOnly();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00048008 File Offset: 0x00047408
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.__emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.__emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.__emptyUniqueKey;
		}

		// Token: 0x170000B7 RID: 183
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00048098 File Offset: 0x00047498
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x000480AC File Offset: 0x000474AC
		internal IEnumerable<SmiMetaDataProperty> Values
		{
			get
			{
				return new List<SmiMetaDataProperty>(this._properties);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000480C4 File Offset: 0x000474C4
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000480D8 File Offset: 0x000474D8
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x04000256 RID: 598
		private const int SelectorCount = 3;

		// Token: 0x04000257 RID: 599
		private SmiMetaDataProperty[] _properties;

		// Token: 0x04000258 RID: 600
		private bool _isReadOnly;

		// Token: 0x04000259 RID: 601
		internal static readonly SmiMetaDataPropertyCollection EmptyInstance;

		// Token: 0x0400025A RID: 602
		private static readonly SmiDefaultFieldsProperty __emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x0400025B RID: 603
		private static readonly SmiOrderProperty __emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x0400025C RID: 604
		private static readonly SmiUniqueKeyProperty __emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());
	}
}
