using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020002F6 RID: 758
	public class RootBuilder : TemplateBuilder
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x00055263 File Offset: 0x00053463
		public RootBuilder()
		{
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00055263 File Offset: 0x00053463
		public RootBuilder(TemplateParser parser)
		{
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000724B4 File Offset: 0x000706B4
		public IDictionary BuiltObjects
		{
			get
			{
				if (this._builtObjects == null)
				{
					this._builtObjects = new Hashtable(RootBuilder.ReferenceKeyComparer.Default);
				}
				return this._builtObjects;
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void OnCodeGenerationComplete()
		{
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000724D4 File Offset: 0x000706D4
		internal void SetTypeMapper(MainTagNameToTypeMapper typeMapper)
		{
			this._typeMapper = typeMapper;
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000724E0 File Offset: 0x000706E0
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			return this._typeMapper.GetControlType(tagName, attribs, true);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000724FD File Offset: 0x000706FD
		internal override void PrepareNoCompilePageSupport()
		{
			base.PrepareNoCompilePageSupport();
			this._typeMapper = null;
		}

		// Token: 0x04001C9E RID: 7326
		private MainTagNameToTypeMapper _typeMapper;

		// Token: 0x04001C9F RID: 7327
		private IDictionary _builtObjects;

		// Token: 0x02000985 RID: 2437
		private class ReferenceKeyComparer : IComparer, IEqualityComparer
		{
			// Token: 0x06006A4A RID: 27210 RVA: 0x0017BBD8 File Offset: 0x00179DD8
			bool IEqualityComparer.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06006A4B RID: 27211 RVA: 0x0017BBDE File Offset: 0x00179DDE
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x06006A4C RID: 27212 RVA: 0x0017BBE6 File Offset: 0x00179DE6
			int IComparer.Compare(object x, object y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x040038BC RID: 14524
			internal static readonly RootBuilder.ReferenceKeyComparer Default = new RootBuilder.ReferenceKeyComparer();
		}
	}
}
