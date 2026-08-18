using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.Axes.BaseUnitSteps
{
	// Token: 0x020003AB RID: 939
	public class BaseUnitSteps : StateManager, IDefaultCheck
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x00075591 File Offset: 0x00073791
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Seconds
		{
			get
			{
				return this.GetList(ref this._seconds);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x0007559F File Offset: 0x0007379F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Minutes
		{
			get
			{
				return this.GetList(ref this._minutes);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000755AD File Offset: 0x000737AD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Hours
		{
			get
			{
				return this.GetList(ref this._hours);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000755BB File Offset: 0x000737BB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Days
		{
			get
			{
				return this.GetList(ref this._days);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000755C9 File Offset: 0x000737C9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Weeks
		{
			get
			{
				return this.GetList(ref this._weeks);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000755D7 File Offset: 0x000737D7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Months
		{
			get
			{
				return this.GetList(ref this._months);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000755E5 File Offset: 0x000737E5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual BaseUnitStepCollection Years
		{
			get
			{
				return this.GetList(ref this._years);
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000755F3 File Offset: 0x000737F3
		public string Serialize()
		{
			return this.Serializer.Serialize(this);
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x00075601 File Offset: 0x00073801
		internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.InitSerializer();
				}
				return this.serializer;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00075620 File Offset: 0x00073820
		protected virtual JavaScriptSerializer InitSerializer()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(this.GetConverters());
			return javaScriptSerializer;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00075640 File Offset: 0x00073840
		public virtual IEnumerable<JavaScriptConverter> GetConverters()
		{
			return new BaseUnitStepsConverter[]
			{
				new BaseUnitStepsConverter()
			};
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00075660 File Offset: 0x00073860
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Seconds.SetDirty();
			this.Minutes.SetDirty();
			this.Hours.SetDirty();
			this.Days.SetDirty();
			this.Weeks.SetDirty();
			this.Months.SetDirty();
			this.Years.SetDirty();
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000756C0 File Offset: 0x000738C0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveStepListState(this.Seconds),
				this.SaveStepListState(this.Minutes),
				this.SaveStepListState(this.Hours),
				this.SaveStepListState(this.Days),
				this.SaveStepListState(this.Weeks),
				this.SaveStepListState(this.Months),
				this.SaveStepListState(this.Years)
			};
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00075748 File Offset: 0x00073948
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			this.LoadStepListState(this.Seconds, array[num++]);
			this.LoadStepListState(this.Minutes, array[num++]);
			this.LoadStepListState(this.Hours, array[num++]);
			this.LoadStepListState(this.Days, array[num++]);
			this.LoadStepListState(this.Weeks, array[num++]);
			this.LoadStepListState(this.Months, array[num++]);
			this.LoadStepListState(this.Years, array[num++]);
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000757F0 File Offset: 0x000739F0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackStepListState(this.Seconds);
			this.TrackStepListState(this.Minutes);
			this.TrackStepListState(this.Hours);
			this.TrackStepListState(this.Days);
			this.TrackStepListState(this.Weeks);
			this.TrackStepListState(this.Months);
			this.TrackStepListState(this.Years);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00075857 File Offset: 0x00073A57
		private object SaveStepListState(BaseUnitStepCollection list)
		{
			return ((IStateManager)list).SaveViewState();
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0007585F File Offset: 0x00073A5F
		private void LoadStepListState(BaseUnitStepCollection list, object state)
		{
			((IStateManager)list).LoadViewState(state);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00075868 File Offset: 0x00073A68
		private void TrackStepListState(BaseUnitStepCollection list)
		{
			((IStateManager)list).TrackViewState();
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00075870 File Offset: 0x00073A70
		public bool IsDefault
		{
			get
			{
				return this.Empty(this.Seconds) && this.Empty(this.Minutes) && this.Empty(this.Hours) && this.Empty(this.Days) && this.Empty(this.Weeks) && this.Empty(this.Months) && this.Empty(this.Years);
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000758DF File Offset: 0x00073ADF
		private BaseUnitStepCollection GetList(ref BaseUnitStepCollection listField)
		{
			if (listField == null)
			{
				listField = new BaseUnitStepCollection();
			}
			return listField;
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000758EE File Offset: 0x00073AEE
		private bool Empty(BaseUnitStepCollection list)
		{
			return list.Count == 0;
		}

		// Token: 0x04000911 RID: 2321
		private BaseUnitStepCollection _seconds;

		// Token: 0x04000912 RID: 2322
		private BaseUnitStepCollection _minutes;

		// Token: 0x04000913 RID: 2323
		private BaseUnitStepCollection _hours;

		// Token: 0x04000914 RID: 2324
		private BaseUnitStepCollection _days;

		// Token: 0x04000915 RID: 2325
		private BaseUnitStepCollection _weeks;

		// Token: 0x04000916 RID: 2326
		private BaseUnitStepCollection _months;

		// Token: 0x04000917 RID: 2327
		private BaseUnitStepCollection _years;

		// Token: 0x04000918 RID: 2328
		private JavaScriptSerializer serializer;
	}
}
