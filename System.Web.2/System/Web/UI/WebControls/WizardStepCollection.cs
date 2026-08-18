using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000516 RID: 1302
	public sealed class WizardStepCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06004210 RID: 16912 RVA: 0x000D829E File Offset: 0x000D649E
		internal WizardStepCollection(Wizard wizard)
		{
			this._wizard = wizard;
			wizard.TemplatedSteps.Clear();
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000D82B8 File Offset: 0x000D64B8
		public int Count
		{
			get
			{
				return this.Views.Count;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x000D82C5 File Offset: 0x000D64C5
		public bool IsReadOnly
		{
			get
			{
				return this.Views.IsReadOnly;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x000D82D2 File Offset: 0x000D64D2
		private ViewCollection Views
		{
			get
			{
				return this._wizard.MultiView.Views;
			}
		}

		// Token: 0x1700136C RID: 4972
		public WizardStepBase this[int index]
		{
			get
			{
				return (WizardStepBase)this.Views[index];
			}
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000D82F8 File Offset: 0x000D64F8
		public void Add(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.PreventAutoID();
			WizardStepCollection.RemoveIfAlreadyExistsInWizard(wizardStep);
			wizardStep.Owner = this._wizard;
			this.Views.Add(wizardStep);
			this.AddTemplatedWizardStep(wizardStep);
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000D8344 File Offset: 0x000D6544
		public void AddAt(int index, WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			WizardStepCollection.RemoveIfAlreadyExistsInWizard(wizardStep);
			wizardStep.PreventAutoID();
			wizardStep.Owner = this._wizard;
			this.Views.AddAt(index, wizardStep);
			this.AddTemplatedWizardStep(wizardStep);
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000D8394 File Offset: 0x000D6594
		private void AddTemplatedWizardStep(WizardStepBase wizardStep)
		{
			TemplatedWizardStep templatedWizardStep = wizardStep as TemplatedWizardStep;
			if (templatedWizardStep != null)
			{
				this._wizard.TemplatedSteps.Add(templatedWizardStep);
				this._wizard.RegisterCustomNavigationContainers(templatedWizardStep);
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x000D83C8 File Offset: 0x000D65C8
		public void Clear()
		{
			this.Views.Clear();
			this._wizard.TemplatedSteps.Clear();
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x000D83EB File Offset: 0x000D65EB
		public bool Contains(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.Views.Contains(wizardStep);
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000D8407 File Offset: 0x000D6607
		public void CopyTo(WizardStepBase[] array, int index)
		{
			this.Views.CopyTo(array, index);
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000D8416 File Offset: 0x000D6616
		public IEnumerator GetEnumerator()
		{
			return this.Views.GetEnumerator();
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000D8423 File Offset: 0x000D6623
		public int IndexOf(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.Views.IndexOf(wizardStep);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000D843F File Offset: 0x000D663F
		public void Insert(int index, WizardStepBase wizardStep)
		{
			this.AddAt(index, wizardStep);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000D8449 File Offset: 0x000D6649
		internal void NotifyWizardStepsChanged()
		{
			this._wizard.OnWizardStepsChanged();
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000D8458 File Offset: 0x000D6658
		public void Remove(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			this.Views.Remove(wizardStep);
			wizardStep.Owner = null;
			TemplatedWizardStep templatedWizardStep = wizardStep as TemplatedWizardStep;
			if (templatedWizardStep != null)
			{
				this._wizard.TemplatedSteps.Remove(templatedWizardStep);
			}
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000D84A8 File Offset: 0x000D66A8
		public void RemoveAt(int index)
		{
			WizardStepBase wizardStepBase = this.Views[index] as WizardStepBase;
			if (wizardStepBase != null)
			{
				wizardStepBase.Owner = null;
				TemplatedWizardStep templatedWizardStep = wizardStepBase as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					this._wizard.TemplatedSteps.Remove(templatedWizardStep);
				}
			}
			this.Views.RemoveAt(index);
			this.NotifyWizardStepsChanged();
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000D84FF File Offset: 0x000D66FF
		private static void RemoveIfAlreadyExistsInWizard(WizardStepBase wizardStep)
		{
			if (wizardStep.Owner != null)
			{
				wizardStep.Owner.WizardSteps.Remove(wizardStep);
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000D851C File Offset: 0x000D671C
		private static WizardStepBase GetStepAndVerify(object value)
		{
			WizardStepBase wizardStepBase = value as WizardStepBase;
			if (wizardStepBase == null)
			{
				throw new ArgumentException(SR.GetString("Wizard_WizardStepOnly"));
			}
			return wizardStepBase;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000D8407 File Offset: 0x000D6607
		void ICollection.CopyTo(Array array, int index)
		{
			this.Views.CopyTo(array, index);
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x00007722 File Offset: 0x00005922
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700136E RID: 4974
		object IList.this[int index]
		{
			get
			{
				return this.Views[index];
			}
			set
			{
				this.RemoveAt(index);
				this.AddAt(index, WizardStepCollection.GetStepAndVerify(value));
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000D8568 File Offset: 0x000D6768
		int IList.Add(object value)
		{
			WizardStepBase stepAndVerify = WizardStepCollection.GetStepAndVerify(value);
			stepAndVerify.PreventAutoID();
			this.Add(stepAndVerify);
			return this.IndexOf(stepAndVerify);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000D8590 File Offset: 0x000D6790
		bool IList.Contains(object value)
		{
			return this.Contains(WizardStepCollection.GetStepAndVerify(value));
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000D859E File Offset: 0x000D679E
		int IList.IndexOf(object value)
		{
			return this.IndexOf(WizardStepCollection.GetStepAndVerify(value));
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000D85AC File Offset: 0x000D67AC
		void IList.Insert(int index, object value)
		{
			this.AddAt(index, WizardStepCollection.GetStepAndVerify(value));
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000D85BB File Offset: 0x000D67BB
		void IList.Remove(object value)
		{
			this.Remove(WizardStepCollection.GetStepAndVerify(value));
		}

		// Token: 0x04002557 RID: 9559
		private Wizard _wizard;
	}
}
