using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000031 RID: 49
	[DefaultProperty("Animations")]
	public abstract class AnimationExtenderControlBase : ExtenderControlBase
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000695C File Offset: 0x00004B5C
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000696D File Offset: 0x00004B6D
		[ExtenderControlProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(MultilineStringConverter))]
		public string Animations
		{
			get
			{
				return this._animations ?? string.Empty;
			}
			set
			{
				if (value != null)
				{
					value = AnimationExtenderControlBase.TrimForDesigner(value);
				}
				if (this._animations != value)
				{
					this._animations = value;
					Animation.Parse(this._animations, this);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000699C File Offset: 0x00004B9C
		private static string TrimForDesigner(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int num = 0;
			while (num < value.Length && char.IsWhiteSpace(value[num]))
			{
				num++;
			}
			num = value.LastIndexOf('\n', num);
			if (num >= 0)
			{
				value = value.Substring(num + 1);
			}
			return value.TrimEnd(new char[0]);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000069F7 File Offset: 0x00004BF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeAnimations()
		{
			return base.DesignMode && !string.IsNullOrEmpty(this._animations);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006A11 File Offset: 0x00004C11
		protected Animation GetAnimation(ref Animation animation, string name)
		{
			if (animation == null)
			{
				animation = Animation.Deserialize(base.GetPropertyValue<string>(name, ""));
			}
			return animation;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006A2C File Offset: 0x00004C2C
		protected void SetAnimation(ref Animation animation, string name, Animation value)
		{
			animation = value;
			base.SetPropertyValue<string>(name, (animation != null) ? animation.ToString() : string.Empty);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006A4C File Offset: 0x00004C4C
		protected void ResolveControlIDs(Animation animation)
		{
			if (animation == null)
			{
				return;
			}
			string text;
			if (animation.Properties.TryGetValue("AnimationTarget", out text) && !string.IsNullOrEmpty(text))
			{
				Control control = null;
				Control control2 = this.NamingContainer;
				while (control2 != null && (control = control2.FindControl(text)) == null)
				{
					control2 = control2.Parent;
				}
				if (control != null)
				{
					animation.Properties["AnimationTarget"] = control.ClientID;
				}
			}
			foreach (Animation animation2 in animation.Children)
			{
				this.ResolveControlIDs(animation2);
			}
		}

		// Token: 0x0400008D RID: 141
		private string _animations;
	}
}
