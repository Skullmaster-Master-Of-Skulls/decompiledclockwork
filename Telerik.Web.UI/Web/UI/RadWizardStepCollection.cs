using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200099F RID: 2463
	public class RadWizardStepCollection : ControlCollection
	{
		// Token: 0x06005DF7 RID: 24055 RVA: 0x0011F4F5 File Offset: 0x0011D6F5
		public RadWizardStepCollection(RadWizard wizard) : base(wizard)
		{
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0011F4FE File Offset: 0x0011D6FE
		public void Add(RadWizardStep wizardStep)
		{
			base.Add(wizardStep);
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x0011F507 File Offset: 0x0011D707
		public void Insert(int index, RadWizardStep wizardStep)
		{
			base.AddAt(index, wizardStep);
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0011F514 File Offset: 0x0011D714
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Add(Control child)
		{
			RadWizardStep radWizardStep = child as RadWizardStep;
			if (radWizardStep == null)
			{
				throw new ArgumentException("RadWizardStepCollection must contain WizardStep objects");
			}
			this.Add(radWizardStep);
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0011F540 File Offset: 0x0011D740
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void AddAt(int index, Control child)
		{
			RadWizardStep radWizardStep = child as RadWizardStep;
			if (radWizardStep == null)
			{
				throw new ArgumentException("RadWizardStepCollection must contain WizardStep objects");
			}
			this.Insert(index, radWizardStep);
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0011F56A File Offset: 0x0011D76A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int IndexOf(Control value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0011F573 File Offset: 0x0011D773
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Remove(Control value)
		{
			base.Remove(value);
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0011F57C File Offset: 0x0011D77C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Contains(Control c)
		{
			return base.Contains(c);
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0011F585 File Offset: 0x0011D785
		public void Remove(RadWizardStep wizardStep)
		{
			base.Remove(wizardStep);
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0011F58E File Offset: 0x0011D78E
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x0011F59D File Offset: 0x0011D79D
		public int IndexOf(RadWizardStep wizardStep)
		{
			return base.IndexOf(wizardStep);
		}

		// Token: 0x17001EF8 RID: 7928
		public RadWizardStep this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (RadWizardStep)base[index];
			}
		}
	}
}
