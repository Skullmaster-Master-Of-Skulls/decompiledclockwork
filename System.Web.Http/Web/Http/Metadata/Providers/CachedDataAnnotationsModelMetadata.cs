using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000137 RID: 311
	public class CachedDataAnnotationsModelMetadata : CachedModelMetadata<CachedDataAnnotationsMetadataAttributes>
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x00019FC8 File Offset: 0x000181C8
		public CachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor) : base(prototype, modelAccessor)
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00019FD2 File Offset: 0x000181D2
		public CachedDataAnnotationsModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, IEnumerable<Attribute> attributes) : base(provider, containerType, modelType, propertyName, new CachedDataAnnotationsMetadataAttributes(attributes))
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00019FE6 File Offset: 0x000181E6
		protected override bool ComputeConvertEmptyStringToNull()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeConvertEmptyStringToNull();
			}
			return base.PrototypeCache.DisplayFormat.ConvertEmptyStringToNull;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001A00C File Offset: 0x0001820C
		protected override string ComputeDescription()
		{
			if (base.PrototypeCache.Display == null)
			{
				return base.ComputeDescription();
			}
			return base.PrototypeCache.Display.GetDescription();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001A034 File Offset: 0x00018234
		protected override bool ComputeIsReadOnly()
		{
			if (base.PrototypeCache.Editable != null)
			{
				return !base.PrototypeCache.Editable.AllowEdit;
			}
			if (base.PrototypeCache.ReadOnly != null)
			{
				return base.PrototypeCache.ReadOnly.IsReadOnly;
			}
			return base.ComputeIsReadOnly();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001A088 File Offset: 0x00018288
		public override string GetDisplayName()
		{
			if (base.PrototypeCache.Display != null)
			{
				string name = base.PrototypeCache.Display.GetName();
				if (name != null)
				{
					return name;
				}
			}
			if (base.PrototypeCache.DisplayName != null)
			{
				string displayName = base.PrototypeCache.DisplayName.DisplayName;
				if (displayName != null)
				{
					return displayName;
				}
			}
			return base.GetDisplayName();
		}
	}
}
