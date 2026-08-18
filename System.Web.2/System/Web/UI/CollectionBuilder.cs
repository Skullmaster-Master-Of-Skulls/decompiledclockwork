using System;
using System.Collections;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000259 RID: 601
	internal sealed class CollectionBuilder : ControlBuilder
	{
		// Token: 0x06001BAB RID: 7083 RVA: 0x000573A0 File Offset: 0x000555A0
		internal CollectionBuilder(bool ignoreUnknownContent)
		{
			this._ignoreUnknownContent = ignoreUnknownContent;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000573B0 File Offset: 0x000555B0
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			PropertyInfo property = TargetFrameworkUtil.GetProperty(parentBuilder.ControlType, tagName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, false);
			base.SetControlType(property.PropertyType);
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			property = TargetFrameworkUtil.GetProperty(base.ControlType, "Item", bindingAttr, null, new Type[]
			{
				typeof(int)
			}, false);
			if (property == null)
			{
				property = TargetFrameworkUtil.GetProperty(base.ControlType, "Item", bindingAttr, null, null, false);
			}
			if (property != null)
			{
				this._itemType = property.PropertyType;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00004335 File Offset: 0x00002535
		public override object BuildObject()
		{
			return this;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00057448 File Offset: 0x00055648
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			Type type = base.Parser.MapStringToType(tagName, attribs);
			if (!(this._itemType != null) || this._itemType.IsAssignableFrom(type))
			{
				return type;
			}
			if (this._ignoreUnknownContent)
			{
				return null;
			}
			string text = string.Empty;
			if (base.ControlType != null)
			{
				text = base.ControlType.FullName;
			}
			else
			{
				text = base.TagName;
			}
			string name = "Invalid_collection_item_type";
			object[] args = new string[]
			{
				text,
				this._itemType.FullName,
				tagName,
				type.FullName
			};
			throw new HttpException(SR.GetString(name, args));
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000574EA File Offset: 0x000556EA
		public override void AppendLiteralString(string s)
		{
			if (this._ignoreUnknownContent)
			{
				return;
			}
			if (!Util.IsWhiteSpaceString(s))
			{
				throw new HttpException(SR.GetString("Literal_content_not_allowed", new object[]
				{
					base.ControlType.FullName,
					s.Trim()
				}));
			}
		}

		// Token: 0x040018CE RID: 6350
		private Type _itemType;

		// Token: 0x040018CF RID: 6351
		private bool _ignoreUnknownContent;
	}
}
