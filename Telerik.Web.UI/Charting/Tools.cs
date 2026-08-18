using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;

namespace Telerik.Charting
{
	// Token: 0x02001747 RID: 5959
	internal sealed class Tools
	{
		// Token: 0x0600E8B2 RID: 59570 RVA: 0x00343E08 File Offset: 0x00342008
		private Tools()
		{
		}

		// Token: 0x0600E8B3 RID: 59571 RVA: 0x00343E10 File Offset: 0x00342010
		internal static bool ParseAttribute(ref string target, XmlNode node, string targetXmlName)
		{
			if (node != null && node.Attributes[targetXmlName] != null)
			{
				target = node.Attributes[targetXmlName].Value;
				return true;
			}
			return false;
		}

		// Token: 0x0600E8B4 RID: 59572 RVA: 0x00343E3C File Offset: 0x0034203C
		internal static void SetAttribute(XmlElement xmlElement, string attributeName, object val, Type attributeType)
		{
			if (val is Enum)
			{
				EnumConverter enumConverter = new EnumConverter(attributeType);
				xmlElement.SetAttribute(attributeName, enumConverter.ConvertToString(val));
				return;
			}
			xmlElement.SetAttribute(attributeName, val.ToString());
		}

		// Token: 0x0600E8B5 RID: 59573 RVA: 0x00343E74 File Offset: 0x00342074
		internal static bool CompareArrays(Color[] a, Color[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			int num = a.Length;
			for (int i = 0; i < num; i++)
			{
				try
				{
					if (!a[i].Equals(b[i]))
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E8B6 RID: 59574 RVA: 0x00343EF0 File Offset: 0x003420F0
		internal static bool CompareArrays(float[] a, float[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			int num = a.Length;
			for (int i = 0; i < num; i++)
			{
				try
				{
					if (a[i] != b[i])
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E8B7 RID: 59575 RVA: 0x00343F50 File Offset: 0x00342150
		internal static float ArraySum(float[] a)
		{
			float num = 0f;
			for (int i = 0; i < a.Length; i++)
			{
				num += a[i];
			}
			return (float)Math.Round((double)num);
		}
	}
}
