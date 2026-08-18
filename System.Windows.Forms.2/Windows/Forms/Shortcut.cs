using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200036A RID: 874
	[ComVisible(true)]
	public enum Shortcut
	{
		// Token: 0x040021F0 RID: 8688
		None,
		// Token: 0x040021F1 RID: 8689
		CtrlA = 131137,
		// Token: 0x040021F2 RID: 8690
		CtrlB,
		// Token: 0x040021F3 RID: 8691
		CtrlC,
		// Token: 0x040021F4 RID: 8692
		CtrlD,
		// Token: 0x040021F5 RID: 8693
		CtrlE,
		// Token: 0x040021F6 RID: 8694
		CtrlF,
		// Token: 0x040021F7 RID: 8695
		CtrlG,
		// Token: 0x040021F8 RID: 8696
		CtrlH,
		// Token: 0x040021F9 RID: 8697
		CtrlI,
		// Token: 0x040021FA RID: 8698
		CtrlJ,
		// Token: 0x040021FB RID: 8699
		CtrlK,
		// Token: 0x040021FC RID: 8700
		CtrlL,
		// Token: 0x040021FD RID: 8701
		CtrlM,
		// Token: 0x040021FE RID: 8702
		CtrlN,
		// Token: 0x040021FF RID: 8703
		CtrlO,
		// Token: 0x04002200 RID: 8704
		CtrlP,
		// Token: 0x04002201 RID: 8705
		CtrlQ,
		// Token: 0x04002202 RID: 8706
		CtrlR,
		// Token: 0x04002203 RID: 8707
		CtrlS,
		// Token: 0x04002204 RID: 8708
		CtrlT,
		// Token: 0x04002205 RID: 8709
		CtrlU,
		// Token: 0x04002206 RID: 8710
		CtrlV,
		// Token: 0x04002207 RID: 8711
		CtrlW,
		// Token: 0x04002208 RID: 8712
		CtrlX,
		// Token: 0x04002209 RID: 8713
		CtrlY,
		// Token: 0x0400220A RID: 8714
		CtrlZ,
		// Token: 0x0400220B RID: 8715
		CtrlShiftA = 196673,
		// Token: 0x0400220C RID: 8716
		CtrlShiftB,
		// Token: 0x0400220D RID: 8717
		CtrlShiftC,
		// Token: 0x0400220E RID: 8718
		CtrlShiftD,
		// Token: 0x0400220F RID: 8719
		CtrlShiftE,
		// Token: 0x04002210 RID: 8720
		CtrlShiftF,
		// Token: 0x04002211 RID: 8721
		CtrlShiftG,
		// Token: 0x04002212 RID: 8722
		CtrlShiftH,
		// Token: 0x04002213 RID: 8723
		CtrlShiftI,
		// Token: 0x04002214 RID: 8724
		CtrlShiftJ,
		// Token: 0x04002215 RID: 8725
		CtrlShiftK,
		// Token: 0x04002216 RID: 8726
		CtrlShiftL,
		// Token: 0x04002217 RID: 8727
		CtrlShiftM,
		// Token: 0x04002218 RID: 8728
		CtrlShiftN,
		// Token: 0x04002219 RID: 8729
		CtrlShiftO,
		// Token: 0x0400221A RID: 8730
		CtrlShiftP,
		// Token: 0x0400221B RID: 8731
		CtrlShiftQ,
		// Token: 0x0400221C RID: 8732
		CtrlShiftR,
		// Token: 0x0400221D RID: 8733
		CtrlShiftS,
		// Token: 0x0400221E RID: 8734
		CtrlShiftT,
		// Token: 0x0400221F RID: 8735
		CtrlShiftU,
		// Token: 0x04002220 RID: 8736
		CtrlShiftV,
		// Token: 0x04002221 RID: 8737
		CtrlShiftW,
		// Token: 0x04002222 RID: 8738
		CtrlShiftX,
		// Token: 0x04002223 RID: 8739
		CtrlShiftY,
		// Token: 0x04002224 RID: 8740
		CtrlShiftZ,
		// Token: 0x04002225 RID: 8741
		F1 = 112,
		// Token: 0x04002226 RID: 8742
		F2,
		// Token: 0x04002227 RID: 8743
		F3,
		// Token: 0x04002228 RID: 8744
		F4,
		// Token: 0x04002229 RID: 8745
		F5,
		// Token: 0x0400222A RID: 8746
		F6,
		// Token: 0x0400222B RID: 8747
		F7,
		// Token: 0x0400222C RID: 8748
		F8,
		// Token: 0x0400222D RID: 8749
		F9,
		// Token: 0x0400222E RID: 8750
		F10,
		// Token: 0x0400222F RID: 8751
		F11,
		// Token: 0x04002230 RID: 8752
		F12,
		// Token: 0x04002231 RID: 8753
		ShiftF1 = 65648,
		// Token: 0x04002232 RID: 8754
		ShiftF2,
		// Token: 0x04002233 RID: 8755
		ShiftF3,
		// Token: 0x04002234 RID: 8756
		ShiftF4,
		// Token: 0x04002235 RID: 8757
		ShiftF5,
		// Token: 0x04002236 RID: 8758
		ShiftF6,
		// Token: 0x04002237 RID: 8759
		ShiftF7,
		// Token: 0x04002238 RID: 8760
		ShiftF8,
		// Token: 0x04002239 RID: 8761
		ShiftF9,
		// Token: 0x0400223A RID: 8762
		ShiftF10,
		// Token: 0x0400223B RID: 8763
		ShiftF11,
		// Token: 0x0400223C RID: 8764
		ShiftF12,
		// Token: 0x0400223D RID: 8765
		CtrlF1 = 131184,
		// Token: 0x0400223E RID: 8766
		CtrlF2,
		// Token: 0x0400223F RID: 8767
		CtrlF3,
		// Token: 0x04002240 RID: 8768
		CtrlF4,
		// Token: 0x04002241 RID: 8769
		CtrlF5,
		// Token: 0x04002242 RID: 8770
		CtrlF6,
		// Token: 0x04002243 RID: 8771
		CtrlF7,
		// Token: 0x04002244 RID: 8772
		CtrlF8,
		// Token: 0x04002245 RID: 8773
		CtrlF9,
		// Token: 0x04002246 RID: 8774
		CtrlF10,
		// Token: 0x04002247 RID: 8775
		CtrlF11,
		// Token: 0x04002248 RID: 8776
		CtrlF12,
		// Token: 0x04002249 RID: 8777
		CtrlShiftF1 = 196720,
		// Token: 0x0400224A RID: 8778
		CtrlShiftF2,
		// Token: 0x0400224B RID: 8779
		CtrlShiftF3,
		// Token: 0x0400224C RID: 8780
		CtrlShiftF4,
		// Token: 0x0400224D RID: 8781
		CtrlShiftF5,
		// Token: 0x0400224E RID: 8782
		CtrlShiftF6,
		// Token: 0x0400224F RID: 8783
		CtrlShiftF7,
		// Token: 0x04002250 RID: 8784
		CtrlShiftF8,
		// Token: 0x04002251 RID: 8785
		CtrlShiftF9,
		// Token: 0x04002252 RID: 8786
		CtrlShiftF10,
		// Token: 0x04002253 RID: 8787
		CtrlShiftF11,
		// Token: 0x04002254 RID: 8788
		CtrlShiftF12,
		// Token: 0x04002255 RID: 8789
		Ins = 45,
		// Token: 0x04002256 RID: 8790
		CtrlIns = 131117,
		// Token: 0x04002257 RID: 8791
		ShiftIns = 65581,
		// Token: 0x04002258 RID: 8792
		Del = 46,
		// Token: 0x04002259 RID: 8793
		CtrlDel = 131118,
		// Token: 0x0400225A RID: 8794
		ShiftDel = 65582,
		// Token: 0x0400225B RID: 8795
		AltRightArrow = 262183,
		// Token: 0x0400225C RID: 8796
		AltLeftArrow = 262181,
		// Token: 0x0400225D RID: 8797
		AltUpArrow,
		// Token: 0x0400225E RID: 8798
		AltDownArrow = 262184,
		// Token: 0x0400225F RID: 8799
		AltBksp = 262152,
		// Token: 0x04002260 RID: 8800
		AltF1 = 262256,
		// Token: 0x04002261 RID: 8801
		AltF2,
		// Token: 0x04002262 RID: 8802
		AltF3,
		// Token: 0x04002263 RID: 8803
		AltF4,
		// Token: 0x04002264 RID: 8804
		AltF5,
		// Token: 0x04002265 RID: 8805
		AltF6,
		// Token: 0x04002266 RID: 8806
		AltF7,
		// Token: 0x04002267 RID: 8807
		AltF8,
		// Token: 0x04002268 RID: 8808
		AltF9,
		// Token: 0x04002269 RID: 8809
		AltF10,
		// Token: 0x0400226A RID: 8810
		AltF11,
		// Token: 0x0400226B RID: 8811
		AltF12,
		// Token: 0x0400226C RID: 8812
		Alt0 = 262192,
		// Token: 0x0400226D RID: 8813
		Alt1,
		// Token: 0x0400226E RID: 8814
		Alt2,
		// Token: 0x0400226F RID: 8815
		Alt3,
		// Token: 0x04002270 RID: 8816
		Alt4,
		// Token: 0x04002271 RID: 8817
		Alt5,
		// Token: 0x04002272 RID: 8818
		Alt6,
		// Token: 0x04002273 RID: 8819
		Alt7,
		// Token: 0x04002274 RID: 8820
		Alt8,
		// Token: 0x04002275 RID: 8821
		Alt9,
		// Token: 0x04002276 RID: 8822
		Ctrl0 = 131120,
		// Token: 0x04002277 RID: 8823
		Ctrl1,
		// Token: 0x04002278 RID: 8824
		Ctrl2,
		// Token: 0x04002279 RID: 8825
		Ctrl3,
		// Token: 0x0400227A RID: 8826
		Ctrl4,
		// Token: 0x0400227B RID: 8827
		Ctrl5,
		// Token: 0x0400227C RID: 8828
		Ctrl6,
		// Token: 0x0400227D RID: 8829
		Ctrl7,
		// Token: 0x0400227E RID: 8830
		Ctrl8,
		// Token: 0x0400227F RID: 8831
		Ctrl9,
		// Token: 0x04002280 RID: 8832
		CtrlShift0 = 196656,
		// Token: 0x04002281 RID: 8833
		CtrlShift1,
		// Token: 0x04002282 RID: 8834
		CtrlShift2,
		// Token: 0x04002283 RID: 8835
		CtrlShift3,
		// Token: 0x04002284 RID: 8836
		CtrlShift4,
		// Token: 0x04002285 RID: 8837
		CtrlShift5,
		// Token: 0x04002286 RID: 8838
		CtrlShift6,
		// Token: 0x04002287 RID: 8839
		CtrlShift7,
		// Token: 0x04002288 RID: 8840
		CtrlShift8,
		// Token: 0x04002289 RID: 8841
		CtrlShift9
	}
}
