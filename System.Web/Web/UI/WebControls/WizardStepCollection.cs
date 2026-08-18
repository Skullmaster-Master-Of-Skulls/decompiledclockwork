using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000685 RID: 1669
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WizardStepCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x0014B612 File Offset: 0x0014A612
		internal WizardStepCollection(Wizard wizard)
		{
			this._wizard = wizard;
			wizard.TemplatedSteps.Clear();
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x0014B62C File Offset: 0x0014A62C
		public int Count
		{
			get
			{
				return this.Views.Count;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x0014B639 File Offset: 0x0014A639
		public bool IsReadOnly
		{
			get
			{
				return this.Views.IsReadOnly;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060051E6 RID: 20966 RVA: 0x0014B646 File Offset: 0x0014A646
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060051E7 RID: 20967 RVA: 0x0014B649 File Offset: 0x0014A649
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x0014B64C File Offset: 0x0014A64C
		private ViewCollection Views
		{
			get
			{
				return this._wizard.MultiView.Views;
			}
		}

		// Token: 0x170014D9 RID: 5337
		public WizardStepBase this[int index]
		{
			get
			{
				return (WizardStepBase)this.Views[index];
			}
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0014B674 File Offset: 0x0014A674
		public void Add(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.PreventAutoID();
			this.RemoveIfAlreadyExistsInWizard(wizardStep);
			wizardStep.Owner = this._wizard;
			this.Views.Add(wizardStep);
			if (wizardStep is TemplatedWizardStep)
			{
				this._wizard.TemplatedSteps.Add(wizardStep);
				this._wizard.RegisterCustomNavigationContainers((TemplatedWizardStep)wizardStep);
			}
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0014B6E8 File Offset: 0x0014A6E8
		public void AddAt(int index, WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			this.RemoveIfAlreadyExistsInWizard(wizardStep);
			wizardStep.PreventAutoID();
			wizardStep.Owner = this._wizard;
			this.Views.AddAt(index, wizardStep);
			if (wizardStep is TemplatedWizardStep)
			{
				this._wizard.TemplatedSteps.Add(wizardStep);
				this._wizard.RegisterCustomNavigationContainers((TemplatedWizardStep)wizardStep);
			}
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0014B75A File Offset: 0x0014A75A
		public void Clear()
		{
			this.Views.Clear();
			this._wizard.TemplatedSteps.Clear();
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0014B77D File Offset: 0x0014A77D
		public bool Contains(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.Views.Contains(wizardStep);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0014B799 File Offset: 0x0014A799
		public void CopyTo(WizardStepBase[] array, int index)
		{
			this.Views.CopyTo(array, index);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0014B7A8 File Offset: 0x0014A7A8
		public IEnumerator GetEnumerator()
		{
			return this.Views.GetEnumerator();
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0014B7B5 File Offset: 0x0014A7B5
		public int IndexOf(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.Views.IndexOf(wizardStep);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0014B7D1 File Offset: 0x0014A7D1
		public void Insert(int index, WizardStepBase wizardStep)
		{
			this.AddAt(index, wizardStep);
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0014B7DB File Offset: 0x0014A7DB
		internal void NotifyWizardStepsChanged()
		{
			this._wizard.OnWizardStepsChanged();
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0014B7E8 File Offset: 0x0014A7E8
		public void Remove(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			this.Views.Remove(wizardStep);
			wizardStep.Owner = null;
			if (wizardStep is TemplatedWizardStep)
			{
				this._wizard.TemplatedSteps.Remove(wizardStep);
			}
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0014B838 File Offset: 0x0014A838
		public void RemoveAt(int index)
		{
			WizardStepBase wizardStepBase = this.Views[index] as WizardStepBase;
			if (wizardStepBase != null)
			{
				wizardStepBase.Owner = null;
				if (wizardStepBase is TemplatedWizardStep)
				{
					this._wizard.TemplatedSteps.Remove(wizardStepBase);
				}
			}
			this.Views.RemoveAt(index);
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0014B88C File Offset: 0x0014A88C
		private void RemoveIfAlreadyExistsInWizard(WizardStepBase wizardStep)
		{
			if (wizardStep.Owner != null)
			{
				wizardStep.Owner.WizardSteps.Remove(wizardStep);
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0014B8A8 File Offset: 0x0014A8A8
		private WizardStepBase GetStepAndVerify(object value)
		{
			WizardStepBase wizardStepBase = value as WizardStepBase;
			if (wizardStepBase == null)
			{
				throw new ArgumentException(SR.GetString("Wizard_WizardStepOnly"));
			}
			return wizardStepBase;
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0014B8D0 File Offset: 0x0014A8D0
		void ICollection.CopyTo(Array array, int index)
		{
			this.Views.CopyTo(array, index);
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x0014B8DF File Offset: 0x0014A8DF
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014DB RID: 5339
		object IList.this[int index]
		{
			get
			{
				return this.Views[index];
			}
			set
			{
				this.RemoveAt(index);
				this.AddAt(index, this.GetStepAndVerify(value));
			}
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0014B908 File Offset: 0x0014A908
		int IList.Add(object value)
		{
			WizardStepBase stepAndVerify = this.GetStepAndVerify(value);
			stepAndVerify.PreventAutoID();
			this.Add(stepAndVerify);
			return this.IndexOf(stepAndVerify);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0014B931 File Offset: 0x0014A931
		bool IList.Contains(object value)
		{
			return this.Contains(this.GetStepAndVerify(value));
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0014B940 File Offset: 0x0014A940
		int IList.IndexOf(object value)
		{
			return this.IndexOf(this.GetStepAndVerify(value));
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0014B94F File Offset: 0x0014A94F
		void IList.Insert(int index, object value)
		{
			this.AddAt(index, this.GetStepAndVerify(value));
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0014B95F File Offset: 0x0014A95F
		void IList.Remove(object value)
		{
			this.Remove(this.GetStepAndVerify(value));
		}

		// Token: 0x04002DD3 RID: 11731
		private Wizard _wizard;
	}
}
