using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication.AuthenticationParameter;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B6 RID: 1462
	public static class AuthenticationAdapter
	{
		// Token: 0x06002F44 RID: 12100 RVA: 0x000341F8 File Offset: 0x000323F8
		public static bool IsExternalUserInfoEmpty(this ExternalUserInfo externalUserInfo)
		{
			bool flag = externalUserInfo == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				string text = (externalUserInfo.StudentNumber ?? "").Trim();
				string text2 = (externalUserInfo.UserName ?? "").Trim();
				string text3 = (externalUserInfo.Email ?? "").Trim();
				result = (text.Length < 1 && text2.Length < 1 && text3.Length < 1);
			}
			return result;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x00034278 File Offset: 0x00032478
		public static string GetExternalUserInfoDisplayString(this ExternalUserInfo externalUserInfo)
		{
			bool flag = externalUserInfo == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				result = string.Concat(new string[]
				{
					"Username=",
					externalUserInfo.UserName ?? "NULL",
					"; StudentNumber=",
					externalUserInfo.StudentNumber ?? "NULL",
					"; Email=",
					externalUserInfo.Email ?? "NULL"
				});
			}
			return result;
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000342F4 File Offset: 0x000324F4
		public static AuthenticationContext GetAuthenticationContextFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			AuthenticationContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				int num;
				IEnumerable<AuthenticationContextItem> source = (from g in xdocument.Root.Elements("AuthenticationContextItem")
				let gType = g.Attribute("type")
				let gTypeInt = (gType == null || string.IsNullOrEmpty(gType.Value) || !int.TryParse(gType.Value, out num)) ? 0 : num
				let gOrderNum = g.Attribute("ordernum")
				select new
				{
					<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
					gIsDisabled = g.Attribute("isdisabled")
				}).Select(delegate(<>h__TransparentIdentifier3)
				{
					AuthenticationContextItem authenticationContextItem = new AuthenticationContextItem();
					authenticationContextItem.ContextItemType = (eAuthenticationContextItemType)(Enum.IsDefined(typeof(eAuthenticationContextItemType), <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.gTypeInt) ? <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.gTypeInt : 0);
					authenticationContextItem.IsDisabled = AuthenticationAdapter.GetBoolFromAttr(<>h__TransparentIdentifier3.gIsDisabled);
					authenticationContextItem.OrderId = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.gOrderNum == null || string.IsNullOrEmpty(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.gOrderNum.Value) || !int.TryParse(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.gOrderNum.Value, out num)) ? 0 : num);
					authenticationContextItem.Args = (from h in g.Elements("arg")
					let hName = h.Attribute("name")
					let hVal = h.Attribute("value")
					select new
					{
						Name = ((hName == null || hName.Value == null) ? "" : hName.Value),
						Val = ((hVal == null || hVal.Value == null) ? "" : hVal.Value)
					}).ToDictionary(q => q.Name, q => q.Val);
					return authenticationContextItem;
				});
				result = new AuthenticationContext
				{
					ContextItems = source.ToList<AuthenticationContextItem>()
				};
			}
			return result;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000343E0 File Offset: 0x000325E0
		private static bool GetBoolFromAttr(XAttribute x)
		{
			bool flag = x == null || x.Value == null || x.Value.Trim().Length < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string text = x.Value.Trim().ToLower();
				bool flag2 = text == "true";
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = text == "false";
					int num;
					result = (!flag3 && int.TryParse(text, out num) && num == 1);
				}
			}
			return result;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00034468 File Offset: 0x00032668
		public static string GetXmlFromAuthenticationContext(this AuthenticationContext context)
		{
			bool flag = context == null;
			if (flag)
			{
				context = new AuthenticationContext();
			}
			bool flag2 = context.ContextItems == null;
			if (flag2)
			{
				context.ContextItems = new List<AuthenticationContextItem>();
			}
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("AuthenticationContext", context.ContextItems.Select(delegate(AuthenticationContextItem r)
			{
				XName name = "AuthenticationContextItem";
				object[] array2 = new object[4];
				array2[0] = new XAttribute("type", ((int)r.ContextItemType).ToString());
				array2[1] = new XAttribute("ordernum", r.OrderId);
				array2[2] = new XAttribute("isdisabled", r.IsDisabled);
				array2[3] = r.Args.Select(delegate(KeyValuePair<string, string> q)
				{
					XName name2 = "arg";
					object[] array3 = new object[2];
					int num = 0;
					XName name3 = "name";
					KeyValuePair<string, string> keyValuePair = q;
					array3[num] = new XAttribute(name3, keyValuePair.Key);
					int num2 = 1;
					XName name4 = "value";
					keyValuePair = q;
					array3[num2] = new XAttribute(name4, keyValuePair.Value ?? "");
					return new XElement(name2, array3);
				});
				return new XElement(name, array2);
			}));
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.ToString();
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00034504 File Offset: 0x00032704
		public static string GetAuthorizationContextDisplayString(this AuthorizationContextItem item)
		{
			bool flag = item == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				string text = (item.Title ?? "").Trim();
				string str = item.ContextItemType.ToString();
				result = text + ((text.Length > 0) ? ": " : "") + str;
			}
			return result;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x00034570 File Offset: 0x00032770
		public static AuthorizationContext GetAuthorizationContextFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			AuthorizationContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				int num;
				IEnumerable<AuthorizationContextItem> source = from g in xdocument.Root.Elements("AuthorizationContextItem")
				let gTitle = g.Attribute("title")
				let gType = g.Attribute("type")
				let gLookupMethod = g.Attribute("lookupmethod")
				let gLookupMethodCid = g.Attribute("lookupmethodcid")
				let gOrderNum = g.Attribute("ordernum")
				let gIsDisabled = g.Attribute("isdisabled")
				let gUsernamePostfix = g.Attribute("usernamepostfix")
				select new AuthorizationContextItem
				{
					Title = ((gTitle == null || gTitle.Value == null) ? "" : gTitle.Value.Trim()),
					ContextItemType = (eAuthorizationContextItemType)((gType == null || string.IsNullOrEmpty(gType.Value) || !int.TryParse(gType.Value, out num) || !Enum.IsDefined(typeof(eAuthorizationContextItemType), num)) ? 0 : num),
					LookupMethod = (eLookupMethod)((gLookupMethod == null || string.IsNullOrEmpty(gLookupMethod.Value) || !int.TryParse(gLookupMethod.Value, out num) || !Enum.IsDefined(typeof(eLookupMethod), num)) ? 0 : num),
					LookupMethodCid = ((gLookupMethodCid == null || string.IsNullOrEmpty(gLookupMethodCid.Value) || !int.TryParse(gLookupMethodCid.Value, out num)) ? 0 : num),
					UsernamePostfix = ((gUsernamePostfix == null || string.IsNullOrEmpty(gUsernamePostfix.Value)) ? "" : gUsernamePostfix.Value.Trim()),
					IsDisabled = (gIsDisabled != null && gIsDisabled.Value != null && int.TryParse(gIsDisabled.Value, out num) && num == 1),
					OrderId = ((gOrderNum == null || string.IsNullOrEmpty(gOrderNum.Value) || !int.TryParse(gOrderNum.Value, out num)) ? 0 : num)
				};
				result = new AuthorizationContext
				{
					ContextItems = source.ToList<AuthorizationContextItem>()
				};
			}
			return result;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000346D8 File Offset: 0x000328D8
		public static string GetXmlFromAuthorizationContext(AuthorizationContext context)
		{
			bool flag = context == null;
			if (flag)
			{
				context = new AuthorizationContext();
			}
			bool flag2 = context.ContextItems == null;
			if (flag2)
			{
				context.ContextItems = new List<AuthorizationContextItem>();
			}
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("AuthorizationContext", from r in context.ContextItems
			select new XElement("AuthorizationContextItem", new object[]
			{
				new XAttribute("title", r.Title ?? ""),
				new XAttribute("type", ((int)r.ContextItemType).ToString()),
				new XAttribute("lookupmethod", (int)r.LookupMethod),
				new XAttribute("lookupmethodcid", r.LookupMethodCid),
				new XAttribute("ordernum", r.OrderId),
				new XAttribute("isdisabled", r.IsDisabled),
				new XAttribute("usernamepostfix", r.UsernamePostfix)
			}));
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.ToString();
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x00034774 File Offset: 0x00032974
		public static TokenIssuerAuthParameter GetTokenIssuerFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			TokenIssuerAuthParameter result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				result = xdocument.Descendants("tokenissuer").Select(delegate(XElement element)
				{
					TokenIssuerAuthParameter tokenIssuerAuthParameter = new TokenIssuerAuthParameter();
					XAttribute xattribute = element.Attribute("name");
					tokenIssuerAuthParameter.Name = (((xattribute != null) ? xattribute.Value : null) ?? "");
					XAttribute xattribute2 = element.Attribute("uri");
					tokenIssuerAuthParameter.UriToken = (((xattribute2 != null) ? xattribute2.Value : null) ?? "");
					XAttribute xattribute3 = element.Attribute("storelocation");
					tokenIssuerAuthParameter.StoreLocation = (((xattribute3 != null) ? xattribute3.Value : null) ?? "");
					XAttribute xattribute4 = element.Attribute("storename");
					tokenIssuerAuthParameter.StoreName = (((xattribute4 != null) ? xattribute4.Value : null) ?? "");
					XAttribute xattribute5 = element.Attribute("findtype");
					tokenIssuerAuthParameter.FindType = (((xattribute5 != null) ? xattribute5.Value : null) ?? "");
					XAttribute xattribute6 = element.Attribute("findvalue");
					tokenIssuerAuthParameter.FindValue = (((xattribute6 != null) ? xattribute6.Value : null) ?? "");
					return tokenIssuerAuthParameter;
				}).FirstOrDefault<TokenIssuerAuthParameter>();
			}
			return result;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000347D4 File Offset: 0x000329D4
		public static string GetXmlFromTokenIssuer(this TokenIssuerAuthParameter tokenIssuer)
		{
			bool flag = tokenIssuer == null;
			if (flag)
			{
				tokenIssuer = new TokenIssuerAuthParameter();
			}
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				new XElement("tokenissuers", new XElement("tokenissuer", new object[]
				{
					new XAttribute("name", tokenIssuer.Name ?? ""),
					new XAttribute("uri", tokenIssuer.UriToken ?? ""),
					new XAttribute("storelocation", tokenIssuer.StoreLocation ?? ""),
					new XAttribute("storename", tokenIssuer.StoreName ?? ""),
					new XAttribute("findtype", tokenIssuer.FindType ?? ""),
					new XAttribute("findvalue", tokenIssuer.FindValue ?? "")
				}))
			});
			return xdocument.ToString();
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00034908 File Offset: 0x00032B08
		public static bool GetContextItemTypeIsVisible(this eAuthenticationContextItemType g)
		{
			AuthenticationContextItemTypeAttribute attribute = g.GetAttribute<AuthenticationContextItemTypeAttribute>();
			return attribute == null || !attribute.IsHidden;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x00034938 File Offset: 0x00032B38
		public static string GetContextItemTypeTitle(this eAuthenticationContextItemType g)
		{
			AuthenticationContextItemTypeAttribute attribute = g.GetAttribute<AuthenticationContextItemTypeAttribute>();
			return (attribute == null) ? "" : (attribute.Title ?? "");
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x00034970 File Offset: 0x00032B70
		public static string GetContextItemTypeDescription(this eAuthenticationContextItemType g)
		{
			AuthenticationContextItemTypeAttribute attribute = g.GetAttribute<AuthenticationContextItemTypeAttribute>();
			bool flag = attribute == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = attribute.Description ?? "";
				bool flag2 = text.Length < 1;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string text2 = attribute.Parameters.GetParametersForDisplay(false, false);
					string text3 = attribute.Parameters.GetParametersForDisplay(true, false);
					bool flag3 = text2.Length > 0;
					if (flag3)
					{
						text2 = "\r\nOptional parameters: " + text2;
					}
					bool flag4 = text3.Length > 0;
					if (flag4)
					{
						text3 = "\r\nRequired parameters: " + text3;
					}
					result = text + text2 + text3;
				}
			}
			return result;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x00034A28 File Offset: 0x00032C28
		public static string GetParametersForDisplay(this AuthenticationContextItemParameter[] parameters, bool showRequired, bool showInHtml)
		{
			string result;
			if (showInHtml)
			{
				result = "<ul>" + string.Join("\r\n", (from p in parameters
				where p.IsRequired == showRequired
				select p).Select(delegate(AuthenticationContextItemParameter h)
				{
					string str = "<li>";
					AuthenticationContextItemParameterAttribute attribute = h.Parameter.GetAttribute<AuthenticationContextItemParameterAttribute>();
					return str + (((attribute != null) ? attribute.ArgName : null) ?? "") + "</li>";
				}).ToArray<string>()) + "</ul>";
			}
			else
			{
				result = string.Join(", ", (from p in parameters
				where p.IsRequired == showRequired
				select p).Select(delegate(AuthenticationContextItemParameter h)
				{
					AuthenticationContextItemParameterAttribute attribute = h.Parameter.GetAttribute<AuthenticationContextItemParameterAttribute>();
					return ((attribute != null) ? attribute.ArgName : null) ?? "";
				}).ToArray<string>());
			}
			return result;
		}
	}
}
