using System;

namespace a.b
{
	// Token: 0x0200025E RID: 606
	internal class fm : co
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x000600CC File Offset: 0x0005F0CC
		public new virtual string g()
		{
			return this.d(this.u.b(34560, 6));
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000600E5 File Offset: 0x0005F0E5
		public new virtual DateTime f()
		{
			return this.f(this.u.b(34566, 6));
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000600FE File Offset: 0x0005F0FE
		public new virtual int j()
		{
			return this.h(this.u.b(34567, 6));
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00060117 File Offset: 0x0005F117
		public new virtual DateTime h()
		{
			return this.f(this.u.b(34568, 6));
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00060130 File Offset: 0x0005F130
		public new virtual int a()
		{
			return this.h(this.u.b(34572, 6));
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00060149 File Offset: 0x0005F149
		public new virtual bool i()
		{
			return this.e(this.u.b(34574, 6));
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00060162 File Offset: 0x0005F162
		public new virtual bool b()
		{
			return this.e(this.u.b(34575, 6));
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0006017B File Offset: 0x0005F17B
		public new virtual bool c()
		{
			return this.e(this.u.b(34576, 6));
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00060194 File Offset: 0x0005F194
		public new virtual bool d()
		{
			return this.e(this.u.b(34577, 6));
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000601AD File Offset: 0x0005F1AD
		public new virtual string e()
		{
			return this.d(this.u.b(34578, 6));
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000601C6 File Offset: 0x0005F1C6
		public fm(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000601D0 File Offset: 0x0005F1D0
		internal fm(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000601E0 File Offset: 0x0005F1E0
		public override string ToString()
		{
			return string.Format("Type ASCII or Unicode string: {0}\nStart Filetime: {1}\nDuration Integer 32-bit signed: {2}\nEnd Filetime: {3}\nLogFlags Integer 32-bit signed: {4}\nDocPrinted Boolean: {5}\nDocSaved Boolean: {6}\nDocRouted Boolean: {7}\nDocPosted Boolean: {8}\nTypeDescription ASCII or Unicode string: {9}", new object[]
			{
				this.g(),
				this.f().ToString("r"),
				this.j(),
				this.h().ToString("r"),
				this.a(),
				this.i(),
				this.b(),
				this.c(),
				this.d(),
				this.e()
			});
		}
	}
}
