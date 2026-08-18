using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x020001DF RID: 479
	public class MultiSelectList : IEnumerable<SelectListItem>, IEnumerable
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00025C81 File Offset: 0x00023E81
		public MultiSelectList(IEnumerable items) : this(items, null)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00025C8B File Offset: 0x00023E8B
		public MultiSelectList(IEnumerable items, IEnumerable selectedValues) : this(items, null, null, selectedValues)
		{
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00025C97 File Offset: 0x00023E97
		public MultiSelectList(IEnumerable items, IEnumerable selectedValues, IEnumerable disabledValues) : this(items, null, null, selectedValues, disabledValues)
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00025CA4 File Offset: 0x00023EA4
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField) : this(items, dataValueField, dataTextField, null)
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00025CB0 File Offset: 0x00023EB0
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, IEnumerable selectedValues) : this(items, dataValueField, dataTextField, null, selectedValues)
		{
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00025CBE File Offset: 0x00023EBE
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField) : this(items, dataValueField, dataTextField, dataGroupField, null)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00025CCC File Offset: 0x00023ECC
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, IEnumerable selectedValues, IEnumerable disabledValues) : this(items, dataValueField, dataTextField, null, selectedValues, disabledValues)
		{
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00025CDC File Offset: 0x00023EDC
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, IEnumerable selectedValues) : this(items, dataValueField, dataTextField, dataGroupField, selectedValues, null)
		{
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00025CEC File Offset: 0x00023EEC
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, IEnumerable selectedValues, IEnumerable disabledValues) : this(items, dataValueField, dataTextField, dataGroupField, selectedValues, disabledValues, null)
		{
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00025D00 File Offset: 0x00023F00
		public MultiSelectList(IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, IEnumerable selectedValues, IEnumerable disabledValues, IEnumerable disabledGroups)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.Items = items;
			this.DataValueField = dataValueField;
			this.DataTextField = dataTextField;
			this.SelectedValues = selectedValues;
			this.DataGroupField = dataGroupField;
			this.DisabledValues = disabledValues;
			this.DisabledGroups = disabledGroups;
			if (this.DataGroupField != null)
			{
				this._groups = new List<SelectListGroup>();
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00025D69 File Offset: 0x00023F69
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00025D71 File Offset: 0x00023F71
		public string DataGroupField { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00025D7A File Offset: 0x00023F7A
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00025D82 File Offset: 0x00023F82
		public string DataTextField { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00025D8B File Offset: 0x00023F8B
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00025D93 File Offset: 0x00023F93
		public string DataValueField { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00025D9C File Offset: 0x00023F9C
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x00025DA4 File Offset: 0x00023FA4
		public IEnumerable DisabledGroups { get; private set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00025DAD File Offset: 0x00023FAD
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00025DB5 File Offset: 0x00023FB5
		public IEnumerable DisabledValues { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00025DBE File Offset: 0x00023FBE
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00025DC6 File Offset: 0x00023FC6
		public IEnumerable Items { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00025DCF File Offset: 0x00023FCF
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x00025DD7 File Offset: 0x00023FD7
		public IEnumerable SelectedValues { get; private set; }

		// Token: 0x06000E61 RID: 3681 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public virtual IEnumerator<SelectListItem> GetEnumerator()
		{
			return this.GetListItems().GetEnumerator();
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00025DED File Offset: 0x00023FED
		internal IList<SelectListItem> GetListItems()
		{
			if (string.IsNullOrEmpty(this.DataValueField))
			{
				return this.GetListItemsWithoutValueField();
			}
			return this.GetListItemsWithValueField();
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00025E94 File Offset: 0x00024094
		private IList<SelectListItem> GetListItemsWithValueField()
		{
			HashSet<string> selectedValues = MultiSelectList.GetStringHashSet(this.SelectedValues);
			HashSet<string> disabledValues = MultiSelectList.GetStringHashSet(this.DisabledValues);
			HashSet<string> disabledGroups = MultiSelectList.GetStringHashSet(this.DisabledGroups);
			IEnumerable<SelectListItem> source = this.Items.Cast<object>().Select(delegate(object item)
			{
				string text = MultiSelectList.Eval(item, this.DataValueField);
				return new SelectListItem
				{
					Group = this.GetGroup(item, disabledGroups),
					Value = text,
					Text = MultiSelectList.Eval(item, this.DataTextField),
					Selected = selectedValues.Contains(text),
					Disabled = disabledValues.Contains(text)
				};
			});
			return source.ToList<SelectListItem>();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00025F74 File Offset: 0x00024174
		private IList<SelectListItem> GetListItemsWithoutValueField()
		{
			HashSet<object> selectedValues = MultiSelectList.GetObjectHashSet(this.SelectedValues);
			HashSet<object> disabledValues = MultiSelectList.GetObjectHashSet(this.DisabledValues);
			HashSet<string> disabledGroups = MultiSelectList.GetStringHashSet(this.DisabledGroups);
			IEnumerable<SelectListItem> source = from object item in this.Items
			select new SelectListItem
			{
				Group = this.GetGroup(item, disabledGroups),
				Text = MultiSelectList.Eval(item, this.DataTextField),
				Selected = selectedValues.Contains(item),
				Disabled = disabledValues.Contains(item)
			};
			return source.ToList<SelectListItem>();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00025FE4 File Offset: 0x000241E4
		private static string Eval(object container, string expression)
		{
			object value = container;
			if (!string.IsNullOrEmpty(expression))
			{
				value = DataBinder.Eval(container, expression);
			}
			return Convert.ToString(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002602C File Offset: 0x0002422C
		private SelectListGroup GetGroup(object container, HashSet<string> disabledGroups)
		{
			if (this._groups == null)
			{
				return null;
			}
			string groupName = MultiSelectList.Eval(container, this.DataGroupField);
			if (string.IsNullOrEmpty(groupName))
			{
				return null;
			}
			SelectListGroup selectListGroup = this._groups.FirstOrDefault((SelectListGroup g) => string.Equals(g.Name, groupName, StringComparison.CurrentCulture));
			if (selectListGroup == null)
			{
				selectListGroup = new SelectListGroup
				{
					Name = groupName,
					Disabled = disabledGroups.Contains(groupName)
				};
				this._groups.Add(selectListGroup);
			}
			return selectListGroup;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000260C8 File Offset: 0x000242C8
		private static HashSet<string> GetStringHashSet(IEnumerable values)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (values != null)
			{
				hashSet.UnionWith(from object value in values
				select Convert.ToString(value, CultureInfo.CurrentCulture));
			}
			return hashSet;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00026114 File Offset: 0x00024314
		private static HashSet<object> GetObjectHashSet(IEnumerable values)
		{
			HashSet<object> hashSet = new HashSet<object>();
			if (values != null)
			{
				hashSet.UnionWith(values.Cast<object>());
			}
			return hashSet;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00026137 File Offset: 0x00024337
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003C1 RID: 961
		private IList<SelectListGroup> _groups;
	}
}
