using System;
using Org.BouncyCastle.Math.EC.Abc;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020002EA RID: 746
	internal class WTauNafMultiplier : ECMultiplier
	{
		// Token: 0x06001B9E RID: 7070 RVA: 0x000A590C File Offset: 0x000A490C
		public ECPoint Multiply(ECPoint point, BigInteger k, PreCompInfo preCompInfo)
		{
			if (!(point is F2mPoint))
			{
				throw new ArgumentException("Only F2mPoint can be used in WTauNafMultiplier");
			}
			F2mPoint f2mPoint = (F2mPoint)point;
			F2mCurve f2mCurve = (F2mCurve)f2mPoint.Curve;
			int m = f2mCurve.M;
			sbyte a = (sbyte)f2mCurve.A.ToBigInteger().IntValue;
			sbyte mu = f2mCurve.GetMu();
			BigInteger[] si = f2mCurve.GetSi();
			ZTauElement lambda = Tnaf.PartModReduction(k, m, a, si, mu, 10);
			return this.MultiplyWTnaf(f2mPoint, lambda, preCompInfo, a, mu);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000A5988 File Offset: 0x000A4988
		private F2mPoint MultiplyWTnaf(F2mPoint p, ZTauElement lambda, PreCompInfo preCompInfo, sbyte a, sbyte mu)
		{
			ZTauElement[] alpha;
			if (a == 0)
			{
				alpha = Tnaf.Alpha0;
			}
			else
			{
				alpha = Tnaf.Alpha1;
			}
			BigInteger tw = Tnaf.GetTw(mu, 4);
			sbyte[] u = Tnaf.TauAdicWNaf(mu, lambda, 4, BigInteger.ValueOf(16L), tw, alpha);
			return WTauNafMultiplier.MultiplyFromWTnaf(p, u, preCompInfo);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000A59CC File Offset: 0x000A49CC
		private static F2mPoint MultiplyFromWTnaf(F2mPoint p, sbyte[] u, PreCompInfo preCompInfo)
		{
			F2mCurve f2mCurve = (F2mCurve)p.Curve;
			sbyte a = (sbyte)f2mCurve.A.ToBigInteger().IntValue;
			F2mPoint[] preComp;
			if (preCompInfo == null || !(preCompInfo is WTauNafPreCompInfo))
			{
				preComp = Tnaf.GetPreComp(p, a);
				p.SetPreCompInfo(new WTauNafPreCompInfo(preComp));
			}
			else
			{
				preComp = ((WTauNafPreCompInfo)preCompInfo).GetPreComp();
			}
			F2mPoint f2mPoint = (F2mPoint)p.Curve.Infinity;
			for (int i = u.Length - 1; i >= 0; i--)
			{
				f2mPoint = Tnaf.Tau(f2mPoint);
				if (u[i] != 0)
				{
					if (u[i] > 0)
					{
						f2mPoint = f2mPoint.AddSimple(preComp[(int)u[i]]);
					}
					else
					{
						f2mPoint = f2mPoint.SubtractSimple(preComp[(int)(-(int)u[i])]);
					}
				}
			}
			return f2mPoint;
		}
	}
}
