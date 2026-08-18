using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200004D RID: 77
	public class ControlDependencyMap
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008D25 File Offset: 0x00006F25
		public static Dictionary<string, ControlDependencyMap> Maps
		{
			get
			{
				return ControlDependencyMap._dependencyMaps.Value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00008D31 File Offset: 0x00006F31
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00008D39 File Offset: 0x00006F39
		public Type Type { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00008D42 File Offset: 0x00006F42
		public IEnumerable<Type> Dependecies
		{
			get
			{
				return this._dependecies;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008D4A File Offset: 0x00006F4A
		public ControlDependencyMap(Type type, Type[] types)
		{
			this.Type = type;
			this._dependecies = new List<Type>(types);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008D68 File Offset: 0x00006F68
		private static Dictionary<string, ControlDependencyMap> CreateDependencyMaps()
		{
			Dictionary<string, ControlDependencyMap> dictionary = new Dictionary<string, ControlDependencyMap>();
			Assembly assembly = typeof(ControlDependencyMap).Assembly;
			string[] array = new string[]
			{
				"AjaxControlToolkit.Accordion",
				"AjaxControlToolkit.AccordionContentPanel",
				"AjaxControlToolkit.AccordionExtender",
				"AjaxControlToolkit.AccordionPane",
				"AjaxControlToolkit.ScriptControlBase",
				"AjaxControlToolkit.AjaxFileUpload",
				"AjaxControlToolkit.AlwaysVisibleControlExtender",
				"AjaxControlToolkit.AnimationExtender",
				"AjaxControlToolkit.AreaChart",
				"AjaxControlToolkit.AsyncFileUpload",
				"AjaxControlToolkit.BarChart",
				"AjaxControlToolkit.BubbleChart",
				"AjaxControlToolkit.AutoCompleteExtender",
				"AjaxControlToolkit.BalloonPopupExtender",
				"AjaxControlToolkit.CalendarExtender",
				"AjaxControlToolkit.CascadingDropDown",
				"AjaxControlToolkit.CollapsiblePanelExtender",
				"AjaxControlToolkit.ColorPickerExtender",
				"AjaxControlToolkit.ComboBox",
				"AjaxControlToolkit.ComboBoxButton",
				"AjaxControlToolkit.ConfirmButtonExtender",
				"AjaxControlToolkit.DragPanelExtender",
				"AjaxControlToolkit.DropDownExtender",
				"AjaxControlToolkit.DropShadowExtender",
				"AjaxControlToolkit.DynamicPopulateExtender",
				"AjaxControlToolkit.FilteredTextBoxExtender",
				"AjaxControlToolkit.Gravatar",
				"AjaxControlToolkit.HoverExtender",
				"AjaxControlToolkit.HoverMenuExtender",
				"AjaxControlToolkit.HtmlEditorExtender",
				"AjaxControlToolkit.HtmlEditor.Editor",
				"AjaxControlToolkit.HtmlEditor.Popups.AttachedPopup",
				"AjaxControlToolkit.HtmlEditor.Popups.AttachedTemplatePopup",
				"AjaxControlToolkit.HtmlEditor.Popups.OkCancelAttachedTemplatePopup",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.MethodButton",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.BackColorClear",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.BackColorSelector",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Bold",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.BulletedList",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Copy",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Cut",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.DecreaseIndent",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.DesignMode",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.DesignModeBoxButton",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.FixedBackColor",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.FixedForeColor",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.FontName",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.FontSize",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.ForeColor",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.ForeColorClear",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.ForeColorSelector",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.HorizontalSeparator",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.HtmlMode",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.IncreaseIndent",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.InsertHR",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.InsertLink",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Italic",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.JustifyCenter",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.JustifyFull",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.JustifyLeft",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.JustifyRight",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Ltr",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.OrderedList",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Paragraph",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Paste",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.PasteText",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.PasteWord",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.PreviewMode",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Redo",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.RemoveAlignment",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.RemoveLink",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.RemoveStyles",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Rtl",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.StrikeThrough",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.SubScript",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.SuperScript",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Underline",
				"AjaxControlToolkit.HtmlEditor.ToolbarButtons.Undo",
				"AjaxControlToolkit.LineChart",
				"AjaxControlToolkit.ListSearchExtender",
				"AjaxControlToolkit.MaskedEditExtender",
				"AjaxControlToolkit.MaskedEditValidator",
				"AjaxControlToolkit.ModalPopupExtender",
				"AjaxControlToolkit.MultiHandleSliderExtender",
				"AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender",
				"AjaxControlToolkit.NoBot",
				"AjaxControlToolkit.NoBotExtender",
				"AjaxControlToolkit.NumericUpDownExtender",
				"AjaxControlToolkit.PagingBulletedListExtender",
				"AjaxControlToolkit.PasswordStrength",
				"AjaxControlToolkit.PieChart",
				"AjaxControlToolkit.PopupControlExtender",
				"AjaxControlToolkit.PopupExtender",
				"AjaxControlToolkit.Rating",
				"AjaxControlToolkit.RatingExtender",
				"AjaxControlToolkit.BulletedList",
				"AjaxControlToolkit.BulletedListItem",
				"AjaxControlToolkit.DraggableListItemExtender",
				"AjaxControlToolkit.DropWatcherExtender",
				"AjaxControlToolkit.ReorderList",
				"AjaxControlToolkit.ReorderListItem",
				"AjaxControlToolkit.ResizableControlExtender",
				"AjaxControlToolkit.Seadragon",
				"AjaxControlToolkit.SeadragonControl",
				"AjaxControlToolkit.SeadragonFixedOverlay",
				"AjaxControlToolkit.SeadragonScalableOverlay",
				"AjaxControlToolkit.SliderExtender",
				"AjaxControlToolkit.TabContainer",
				"AjaxControlToolkit.TabPanel",
				"AjaxControlToolkit.ToggleButtonExtender",
				"AjaxControlToolkit.RoundedCornersExtender",
				"AjaxControlToolkit.SlideShowExtender",
				"AjaxControlToolkit.TextBoxWatermarkExtender",
				"AjaxControlToolkit.Twitter",
				"AjaxControlToolkit.UpdatePanelAnimationExtender",
				"AjaxControlToolkit.ValidatorCalloutExtender"
			};
			foreach (string text in array)
			{
				Type type = assembly.GetType(text);
				dictionary[text] = ControlDependencyMap.BuildDependencyMap(type);
			}
			foreach (Type type2 in ToolkitConfig.CustomControls)
			{
				dictionary[type2.FullName] = ControlDependencyMap.BuildDependencyMap(type2);
			}
			return dictionary;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000092DC File Offset: 0x000074DC
		public static ControlDependencyMap BuildDependencyMap(Type type)
		{
			List<Type> source = new List<Type>();
			ControlDependencyMap.SeekDependencies(type, ref source);
			List<Type> list = (from m in source
			where m.GetCustomAttributes(true).Any((object a) => a is RequiredScriptAttribute || a is ClientScriptResourceAttribute)
			select m).ToList<Type>();
			list.Add(type);
			return new ControlDependencyMap(type, list.Distinct<Type>().ToArray<Type>());
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000938C File Offset: 0x0000758C
		private static void SeekDependencies(Type ctlType, ref List<Type> dependencies)
		{
			List<Type> deps = dependencies;
			List<Type> list = (from m in (from m in ctlType.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).SelectMany((MemberInfo info) => ControlDependencyMap.GetMemberTypes(info))
			where m != null && m.Namespace != null && m.Namespace.StartsWith("AjaxControlToolkit")
			select m).Distinct<Type>()
			where !deps.Contains(m)
			select m).ToList<Type>();
			foreach (object obj in from a in ctlType.GetCustomAttributes(true)
			where a is TargetControlTypeAttribute
			select a)
			{
				Type targetControlType = ((TargetControlTypeAttribute)obj).TargetControlType;
				if (!list.Contains(targetControlType) && !dependencies.Contains(targetControlType))
				{
					list.Add(targetControlType);
				}
			}
			dependencies.AddRange(list.ToList<Type>());
			foreach (Type ctlType2 in list)
			{
				ControlDependencyMap.SeekDependencies(ctlType2, ref dependencies);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009500 File Offset: 0x00007700
		private static IEnumerable<Type> GetMemberTypes(MemberInfo memberInfo)
		{
			Type type = null;
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType <= MemberTypes.Method)
			{
				switch (memberType)
				{
				case MemberTypes.Event:
					type = ((EventInfo)memberInfo).EventHandlerType;
					break;
				case MemberTypes.Constructor | MemberTypes.Event:
					break;
				case MemberTypes.Field:
					type = ((FieldInfo)memberInfo).FieldType;
					break;
				default:
					if (memberType == MemberTypes.Method)
					{
						type = ((MethodInfo)memberInfo).ReturnType;
					}
					break;
				}
			}
			else if (memberType != MemberTypes.Property)
			{
				if (memberType == MemberTypes.NestedType)
				{
					MemberInfo[] members = ((Type)memberInfo).GetMembers();
					List<Type> ntypes = new List<Type>();
					foreach (MemberInfo memberInfo2 in members)
					{
						IEnumerable<Type> collection = from x in ControlDependencyMap.GetMemberTypes(memberInfo2)
						where !ntypes.Contains(x)
						select x;
						ntypes.AddRange(collection);
					}
					return ntypes.ToArray();
				}
			}
			else
			{
				type = ((PropertyInfo)memberInfo).PropertyType;
			}
			return new Type[]
			{
				type
			};
		}

		// Token: 0x040000E9 RID: 233
		private static Lazy<Dictionary<string, ControlDependencyMap>> _dependencyMaps = new Lazy<Dictionary<string, ControlDependencyMap>>(new Func<Dictionary<string, ControlDependencyMap>>(ControlDependencyMap.CreateDependencyMaps), true);

		// Token: 0x040000EA RID: 234
		private List<Type> _dependecies;
	}
}
