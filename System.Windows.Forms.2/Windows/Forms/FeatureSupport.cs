using System;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200024E RID: 590
	public abstract class FeatureSupport : IFeatureSupport
	{
		// Token: 0x06002552 RID: 9554 RVA: 0x000AE493 File Offset: 0x000AC693
		public static bool IsPresent(string featureClassName, string featureConstName)
		{
			return FeatureSupport.IsPresent(featureClassName, featureConstName, new Version(0, 0, 0, 0));
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000AE4A8 File Offset: 0x000AC6A8
		public static bool IsPresent(string featureClassName, string featureConstName, Version minimumVersion)
		{
			object obj = null;
			Type type = null;
			try
			{
				type = Type.GetType(featureClassName);
			}
			catch (ArgumentException)
			{
			}
			if (type != null)
			{
				FieldInfo field = type.GetField(featureConstName);
				if (field != null)
				{
					obj = field.GetValue(null);
				}
			}
			if (obj != null && typeof(IFeatureSupport).IsAssignableFrom(type))
			{
				IFeatureSupport featureSupport = (IFeatureSupport)SecurityUtils.SecureCreateInstance(type);
				if (featureSupport != null)
				{
					return featureSupport.IsPresent(obj, minimumVersion);
				}
			}
			return false;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000AE528 File Offset: 0x000AC728
		public static Version GetVersionPresent(string featureClassName, string featureConstName)
		{
			object obj = null;
			Type type = null;
			try
			{
				type = Type.GetType(featureClassName);
			}
			catch (ArgumentException)
			{
			}
			if (type != null)
			{
				FieldInfo field = type.GetField(featureConstName);
				if (field != null)
				{
					obj = field.GetValue(null);
				}
			}
			if (obj != null)
			{
				IFeatureSupport featureSupport = (IFeatureSupport)SecurityUtils.SecureCreateInstance(type);
				if (featureSupport != null)
				{
					return featureSupport.GetVersionPresent(obj);
				}
			}
			return null;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000AE594 File Offset: 0x000AC794
		public virtual bool IsPresent(object feature)
		{
			return this.IsPresent(feature, new Version(0, 0, 0, 0));
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000AE5A8 File Offset: 0x000AC7A8
		public virtual bool IsPresent(object feature, Version minimumVersion)
		{
			Version versionPresent = this.GetVersionPresent(feature);
			return versionPresent != null && versionPresent.CompareTo(minimumVersion) >= 0;
		}

		// Token: 0x06002557 RID: 9559
		public abstract Version GetVersionPresent(object feature);
	}
}
