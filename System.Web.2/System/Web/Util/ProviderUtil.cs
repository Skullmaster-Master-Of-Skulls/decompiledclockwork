using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000216 RID: 534
	internal static class ProviderUtil
	{
		// Token: 0x060019CF RID: 6607 RVA: 0x0005098A File Offset: 0x0004EB8A
		internal static void GetAndRemoveStringAttribute(NameValueCollection config, string attrib, string providerName, ref string val)
		{
			val = config.Get(attrib);
			config.Remove(attrib);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0005099C File Offset: 0x0004EB9C
		internal static void GetAndRemovePositiveAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			ProviderUtil.GetPositiveAttribute(config, attrib, providerName, ref val);
			config.Remove(attrib);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000509B0 File Offset: 0x0004EBB0
		internal static void GetPositiveAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			string text = config.Get(attrib);
			if (text == null)
			{
				return;
			}
			int num;
			try
			{
				num = Convert.ToInt32(text, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is FormatException || ex is OverflowException)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_positive_attributes", new object[]
					{
						attrib,
						providerName
					}));
				}
				throw;
			}
			if (num < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_positive_attributes", new object[]
				{
					attrib,
					providerName
				}));
			}
			val = num;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00050A48 File Offset: 0x0004EC48
		internal static void GetAndRemovePositiveOrInfiniteAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			ProviderUtil.GetPositiveOrInfiniteAttribute(config, attrib, providerName, ref val);
			config.Remove(attrib);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00050A5C File Offset: 0x0004EC5C
		internal static void GetPositiveOrInfiniteAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			string text = config.Get(attrib);
			if (text == null)
			{
				return;
			}
			int num;
			if (text == "Infinite")
			{
				num = int.MaxValue;
			}
			else
			{
				try
				{
					num = Convert.ToInt32(text, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					if (ex is ArgumentException || ex is FormatException || ex is OverflowException)
					{
						throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_positive_attributes", new object[]
						{
							attrib,
							providerName
						}));
					}
					throw;
				}
				if (num < 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_positive_attributes", new object[]
					{
						attrib,
						providerName
					}));
				}
			}
			val = num;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00050B08 File Offset: 0x0004ED08
		internal static void GetAndRemoveNonZeroPositiveOrInfiniteAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			ProviderUtil.GetNonZeroPositiveOrInfiniteAttribute(config, attrib, providerName, ref val);
			config.Remove(attrib);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00050B1C File Offset: 0x0004ED1C
		internal static void GetNonZeroPositiveOrInfiniteAttribute(NameValueCollection config, string attrib, string providerName, ref int val)
		{
			string text = config.Get(attrib);
			if (text == null)
			{
				return;
			}
			int num;
			if (text == "Infinite")
			{
				num = int.MaxValue;
			}
			else
			{
				try
				{
					num = Convert.ToInt32(text, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					if (ex is ArgumentException || ex is FormatException || ex is OverflowException)
					{
						throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_non_zero_positive_attributes", new object[]
						{
							attrib,
							providerName
						}));
					}
					throw;
				}
				if (num <= 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_non_zero_positive_attributes", new object[]
					{
						attrib,
						providerName
					}));
				}
			}
			val = num;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00050BC8 File Offset: 0x0004EDC8
		internal static void GetAndRemoveBooleanAttribute(NameValueCollection config, string attrib, string providerName, ref bool val)
		{
			ProviderUtil.GetBooleanAttribute(config, attrib, providerName, ref val);
			config.Remove(attrib);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00050BDC File Offset: 0x0004EDDC
		internal static void GetBooleanAttribute(NameValueCollection config, string attrib, string providerName, ref bool val)
		{
			string text = config.Get(attrib);
			if (text == null)
			{
				return;
			}
			if (text == "true")
			{
				val = true;
				return;
			}
			if (text == "false")
			{
				val = false;
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Invalid_provider_attribute", new object[]
			{
				attrib,
				providerName,
				text
			}));
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00050C38 File Offset: 0x0004EE38
		internal static void GetAndRemoveRequiredNonEmptyStringAttribute(NameValueCollection config, string attrib, string providerName, ref string val)
		{
			ProviderUtil.GetRequiredNonEmptyStringAttribute(config, attrib, providerName, ref val);
			config.Remove(attrib);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00050C4A File Offset: 0x0004EE4A
		internal static void GetRequiredNonEmptyStringAttribute(NameValueCollection config, string attrib, string providerName, ref string val)
		{
			ProviderUtil.GetNonEmptyStringAttributeInternal(config, attrib, providerName, ref val, true);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00050C58 File Offset: 0x0004EE58
		private static void GetNonEmptyStringAttributeInternal(NameValueCollection config, string attrib, string providerName, ref string val, bool required)
		{
			string text = config.Get(attrib);
			if ((text == null && required) || text.Length == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Provider_missing_attribute", new object[]
				{
					attrib,
					providerName
				}));
			}
			val = text;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00050CA0 File Offset: 0x0004EEA0
		internal static void CheckUnrecognizedAttributes(NameValueCollection config, string providerName)
		{
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					throw new ConfigurationErrorsException(SR.GetString("Unexpected_provider_attribute", new object[]
					{
						key,
						providerName
					}));
				}
			}
		}

		// Token: 0x040017EF RID: 6127
		internal const int Infinite = 2147483647;
	}
}
