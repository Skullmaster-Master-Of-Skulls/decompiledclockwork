using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BDC RID: 3036
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F50F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLDivPosition
	{
		// Token: 0x06014132 RID: 82226
		[DispId(-2147417611)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void setAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06014133 RID: 82227
		[DispId(-2147417610)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object getAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06014134 RID: 82228
		[DispId(-2147417609)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool removeAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006A2B RID: 27179
		// (get) Token: 0x06014136 RID: 82230
		// (set) Token: 0x06014135 RID: 82229
		[DispId(-2147417111)]
		string className { [TypeLibFunc(4)] [DispId(-2147417111)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417111)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A2C RID: 27180
		// (get) Token: 0x06014138 RID: 82232
		// (set) Token: 0x06014137 RID: 82231
		[DispId(-2147417110)]
		string id { [DispId(-2147417110)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417110)] [TypeLibFunc(4)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A2D RID: 27181
		// (get) Token: 0x06014139 RID: 82233
		[DispId(-2147417108)]
		string tagName { [DispId(-2147417108)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17006A2E RID: 27182
		// (get) Token: 0x0601413A RID: 82234
		[DispId(-2147418104)]
		IHTMLElement parentElement { [DispId(-2147418104)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A2F RID: 27183
		// (get) Token: 0x0601413B RID: 82235
		[DispId(-2147418038)]
		IHTMLStyle style { [TypeLibFunc(1024)] [DispId(-2147418038)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A30 RID: 27184
		// (get) Token: 0x0601413D RID: 82237
		// (set) Token: 0x0601413C RID: 82236
		[DispId(-2147412099)]
		object onhelp { [TypeLibFunc(20)] [DispId(-2147412099)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412099)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A31 RID: 27185
		// (get) Token: 0x0601413F RID: 82239
		// (set) Token: 0x0601413E RID: 82238
		[DispId(-2147412104)]
		object onclick { [TypeLibFunc(20)] [DispId(-2147412104)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412104)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A32 RID: 27186
		// (get) Token: 0x06014141 RID: 82241
		// (set) Token: 0x06014140 RID: 82240
		[DispId(-2147412103)]
		object ondblclick { [TypeLibFunc(20)] [DispId(-2147412103)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412103)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A33 RID: 27187
		// (get) Token: 0x06014143 RID: 82243
		// (set) Token: 0x06014142 RID: 82242
		[DispId(-2147412107)]
		object onkeydown { [DispId(-2147412107)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412107)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A34 RID: 27188
		// (get) Token: 0x06014145 RID: 82245
		// (set) Token: 0x06014144 RID: 82244
		[DispId(-2147412106)]
		object onkeyup { [TypeLibFunc(20)] [DispId(-2147412106)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412106)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A35 RID: 27189
		// (get) Token: 0x06014147 RID: 82247
		// (set) Token: 0x06014146 RID: 82246
		[DispId(-2147412105)]
		object onkeypress { [TypeLibFunc(20)] [DispId(-2147412105)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412105)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A36 RID: 27190
		// (get) Token: 0x06014149 RID: 82249
		// (set) Token: 0x06014148 RID: 82248
		[DispId(-2147412111)]
		object onmouseout { [TypeLibFunc(20)] [DispId(-2147412111)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412111)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A37 RID: 27191
		// (get) Token: 0x0601414B RID: 82251
		// (set) Token: 0x0601414A RID: 82250
		[DispId(-2147412112)]
		object onmouseover { [TypeLibFunc(20)] [DispId(-2147412112)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412112)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A38 RID: 27192
		// (get) Token: 0x0601414D RID: 82253
		// (set) Token: 0x0601414C RID: 82252
		[DispId(-2147412108)]
		object onmousemove { [DispId(-2147412108)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412108)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A39 RID: 27193
		// (get) Token: 0x0601414F RID: 82255
		// (set) Token: 0x0601414E RID: 82254
		[DispId(-2147412110)]
		object onmousedown { [DispId(-2147412110)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412110)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A3A RID: 27194
		// (get) Token: 0x06014151 RID: 82257
		// (set) Token: 0x06014150 RID: 82256
		[DispId(-2147412109)]
		object onmouseup { [DispId(-2147412109)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412109)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A3B RID: 27195
		// (get) Token: 0x06014152 RID: 82258
		[DispId(-2147417094)]
		object document { [DispId(-2147417094)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006A3C RID: 27196
		// (get) Token: 0x06014154 RID: 82260
		// (set) Token: 0x06014153 RID: 82259
		[DispId(-2147418043)]
		string title { [DispId(-2147418043)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147418043)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A3D RID: 27197
		// (get) Token: 0x06014156 RID: 82262
		// (set) Token: 0x06014155 RID: 82261
		[DispId(-2147413012)]
		string language { [TypeLibFunc(20)] [DispId(-2147413012)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147413012)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A3E RID: 27198
		// (get) Token: 0x06014158 RID: 82264
		// (set) Token: 0x06014157 RID: 82263
		[DispId(-2147412075)]
		object onselectstart { [DispId(-2147412075)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412075)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x06014159 RID: 82265
		[DispId(-2147417093)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void scrollIntoView([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varargStart);

		// Token: 0x0601415A RID: 82266
		[DispId(-2147417092)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool contains([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement pChild);

		// Token: 0x17006A3F RID: 27199
		// (get) Token: 0x0601415B RID: 82267
		[DispId(-2147417088)]
		int sourceIndex { [TypeLibFunc(4)] [DispId(-2147417088)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A40 RID: 27200
		// (get) Token: 0x0601415C RID: 82268
		[DispId(-2147417087)]
		object recordNumber { [DispId(-2147417087)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; }

		// Token: 0x17006A41 RID: 27201
		// (get) Token: 0x0601415E RID: 82270
		// (set) Token: 0x0601415D RID: 82269
		[DispId(-2147413103)]
		string lang { [DispId(-2147413103)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147413103)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A42 RID: 27202
		// (get) Token: 0x0601415F RID: 82271
		[DispId(-2147417104)]
		int offsetLeft { [DispId(-2147417104)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A43 RID: 27203
		// (get) Token: 0x06014160 RID: 82272
		[DispId(-2147417103)]
		int offsetTop { [DispId(-2147417103)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A44 RID: 27204
		// (get) Token: 0x06014161 RID: 82273
		[DispId(-2147417102)]
		int offsetWidth { [DispId(-2147417102)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A45 RID: 27205
		// (get) Token: 0x06014162 RID: 82274
		[DispId(-2147417101)]
		int offsetHeight { [DispId(-2147417101)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A46 RID: 27206
		// (get) Token: 0x06014163 RID: 82275
		[DispId(-2147417100)]
		IHTMLElement offsetParent { [DispId(-2147417100)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A47 RID: 27207
		// (get) Token: 0x06014165 RID: 82277
		// (set) Token: 0x06014164 RID: 82276
		[DispId(-2147417086)]
		string innerHTML { [DispId(-2147417086)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417086)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A48 RID: 27208
		// (get) Token: 0x06014167 RID: 82279
		// (set) Token: 0x06014166 RID: 82278
		[DispId(-2147417085)]
		string innerText { [DispId(-2147417085)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417085)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A49 RID: 27209
		// (get) Token: 0x06014169 RID: 82281
		// (set) Token: 0x06014168 RID: 82280
		[DispId(-2147417084)]
		string outerHTML { [DispId(-2147417084)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417084)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A4A RID: 27210
		// (get) Token: 0x0601416B RID: 82283
		// (set) Token: 0x0601416A RID: 82282
		[DispId(-2147417083)]
		string outerText { [DispId(-2147417083)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417083)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x0601416C RID: 82284
		[DispId(-2147417082)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void insertAdjacentHTML([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string html);

		// Token: 0x0601416D RID: 82285
		[DispId(-2147417081)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void insertAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string text);

		// Token: 0x17006A4B RID: 27211
		// (get) Token: 0x0601416E RID: 82286
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit { [DispId(-2147417080)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A4C RID: 27212
		// (get) Token: 0x0601416F RID: 82287
		[DispId(-2147417078)]
		bool isTextEdit { [DispId(-2147417078)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x06014170 RID: 82288
		[DispId(-2147417079)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void click();

		// Token: 0x17006A4D RID: 27213
		// (get) Token: 0x06014171 RID: 82289
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters { [DispId(-2147417077)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A4E RID: 27214
		// (get) Token: 0x06014173 RID: 82291
		// (set) Token: 0x06014172 RID: 82290
		[DispId(-2147412077)]
		object ondragstart { [DispId(-2147412077)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412077)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x06014174 RID: 82292
		[DispId(-2147417076)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string toString();

		// Token: 0x17006A4F RID: 27215
		// (get) Token: 0x06014176 RID: 82294
		// (set) Token: 0x06014175 RID: 82293
		[DispId(-2147412091)]
		object onbeforeupdate { [DispId(-2147412091)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412091)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A50 RID: 27216
		// (get) Token: 0x06014178 RID: 82296
		// (set) Token: 0x06014177 RID: 82295
		[DispId(-2147412090)]
		object onafterupdate { [DispId(-2147412090)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412090)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A51 RID: 27217
		// (get) Token: 0x0601417A RID: 82298
		// (set) Token: 0x06014179 RID: 82297
		[DispId(-2147412074)]
		object onerrorupdate { [DispId(-2147412074)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412074)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A52 RID: 27218
		// (get) Token: 0x0601417C RID: 82300
		// (set) Token: 0x0601417B RID: 82299
		[DispId(-2147412094)]
		object onrowexit { [TypeLibFunc(20)] [DispId(-2147412094)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412094)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A53 RID: 27219
		// (get) Token: 0x0601417E RID: 82302
		// (set) Token: 0x0601417D RID: 82301
		[DispId(-2147412093)]
		object onrowenter { [DispId(-2147412093)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412093)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A54 RID: 27220
		// (get) Token: 0x06014180 RID: 82304
		// (set) Token: 0x0601417F RID: 82303
		[DispId(-2147412072)]
		object ondatasetchanged { [TypeLibFunc(20)] [DispId(-2147412072)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412072)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A55 RID: 27221
		// (get) Token: 0x06014182 RID: 82306
		// (set) Token: 0x06014181 RID: 82305
		[DispId(-2147412071)]
		object ondataavailable { [DispId(-2147412071)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412071)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A56 RID: 27222
		// (get) Token: 0x06014184 RID: 82308
		// (set) Token: 0x06014183 RID: 82307
		[DispId(-2147412070)]
		object ondatasetcomplete { [TypeLibFunc(20)] [DispId(-2147412070)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412070)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A57 RID: 27223
		// (get) Token: 0x06014186 RID: 82310
		// (set) Token: 0x06014185 RID: 82309
		[DispId(-2147412069)]
		object onfilterchange { [DispId(-2147412069)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412069)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A58 RID: 27224
		// (get) Token: 0x06014187 RID: 82311
		[DispId(-2147417075)]
		object children { [DispId(-2147417075)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006A59 RID: 27225
		// (get) Token: 0x06014188 RID: 82312
		[DispId(-2147417074)]
		object all { [DispId(-2147417074)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006A5A RID: 27226
		// (get) Token: 0x06014189 RID: 82313
		[DispId(-2147417073)]
		string scopeName { [DispId(-2147417073)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x0601418A RID: 82314
		[DispId(-2147417072)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x0601418B RID: 82315
		[DispId(-2147417071)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void releaseCapture();

		// Token: 0x17006A5B RID: 27227
		// (get) Token: 0x0601418D RID: 82317
		// (set) Token: 0x0601418C RID: 82316
		[DispId(-2147412066)]
		object onlosecapture { [DispId(-2147412066)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412066)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x0601418E RID: 82318
		[DispId(-2147417070)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0601418F RID: 82319
		[DispId(-2147417069)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void doScroll([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object component);

		// Token: 0x17006A5C RID: 27228
		// (get) Token: 0x06014191 RID: 82321
		// (set) Token: 0x06014190 RID: 82320
		[DispId(-2147412081)]
		object onscroll { [TypeLibFunc(20)] [DispId(-2147412081)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412081)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A5D RID: 27229
		// (get) Token: 0x06014193 RID: 82323
		// (set) Token: 0x06014192 RID: 82322
		[DispId(-2147412063)]
		object ondrag { [DispId(-2147412063)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412063)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A5E RID: 27230
		// (get) Token: 0x06014195 RID: 82325
		// (set) Token: 0x06014194 RID: 82324
		[DispId(-2147412062)]
		object ondragend { [DispId(-2147412062)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412062)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A5F RID: 27231
		// (get) Token: 0x06014197 RID: 82327
		// (set) Token: 0x06014196 RID: 82326
		[DispId(-2147412061)]
		object ondragenter { [DispId(-2147412061)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412061)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A60 RID: 27232
		// (get) Token: 0x06014199 RID: 82329
		// (set) Token: 0x06014198 RID: 82328
		[DispId(-2147412060)]
		object ondragover { [TypeLibFunc(20)] [DispId(-2147412060)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412060)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A61 RID: 27233
		// (get) Token: 0x0601419B RID: 82331
		// (set) Token: 0x0601419A RID: 82330
		[DispId(-2147412059)]
		object ondragleave { [TypeLibFunc(20)] [DispId(-2147412059)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412059)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A62 RID: 27234
		// (get) Token: 0x0601419D RID: 82333
		// (set) Token: 0x0601419C RID: 82332
		[DispId(-2147412058)]
		object ondrop { [TypeLibFunc(20)] [DispId(-2147412058)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412058)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A63 RID: 27235
		// (get) Token: 0x0601419F RID: 82335
		// (set) Token: 0x0601419E RID: 82334
		[DispId(-2147412054)]
		object onbeforecut { [TypeLibFunc(20)] [DispId(-2147412054)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412054)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A64 RID: 27236
		// (get) Token: 0x060141A1 RID: 82337
		// (set) Token: 0x060141A0 RID: 82336
		[DispId(-2147412057)]
		object oncut { [TypeLibFunc(20)] [DispId(-2147412057)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412057)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A65 RID: 27237
		// (get) Token: 0x060141A3 RID: 82339
		// (set) Token: 0x060141A2 RID: 82338
		[DispId(-2147412053)]
		object onbeforecopy { [TypeLibFunc(20)] [DispId(-2147412053)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412053)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A66 RID: 27238
		// (get) Token: 0x060141A5 RID: 82341
		// (set) Token: 0x060141A4 RID: 82340
		[DispId(-2147412056)]
		object oncopy { [TypeLibFunc(20)] [DispId(-2147412056)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412056)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A67 RID: 27239
		// (get) Token: 0x060141A7 RID: 82343
		// (set) Token: 0x060141A6 RID: 82342
		[DispId(-2147412052)]
		object onbeforepaste { [DispId(-2147412052)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412052)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A68 RID: 27240
		// (get) Token: 0x060141A9 RID: 82345
		// (set) Token: 0x060141A8 RID: 82344
		[DispId(-2147412055)]
		object onpaste { [TypeLibFunc(20)] [DispId(-2147412055)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412055)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A69 RID: 27241
		// (get) Token: 0x060141AA RID: 82346
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle { [DispId(-2147417105)] [TypeLibFunc(1024)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A6A RID: 27242
		// (get) Token: 0x060141AC RID: 82348
		// (set) Token: 0x060141AB RID: 82347
		[DispId(-2147412065)]
		object onpropertychange { [TypeLibFunc(20)] [DispId(-2147412065)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412065)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x060141AD RID: 82349
		[DispId(-2147417068)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLRectCollection getClientRects();

		// Token: 0x060141AE RID: 82350
		[DispId(-2147417067)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x060141AF RID: 82351
		[DispId(-2147417608)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void setExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname, [MarshalAs(UnmanagedType.BStr)] [In] string expression, [MarshalAs(UnmanagedType.BStr)] [In] string language = "");

		// Token: 0x060141B0 RID: 82352
		[DispId(-2147417607)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object getExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname);

		// Token: 0x060141B1 RID: 82353
		[DispId(-2147417606)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool removeExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname);

		// Token: 0x17006A6B RID: 27243
		// (get) Token: 0x060141B3 RID: 82355
		// (set) Token: 0x060141B2 RID: 82354
		[DispId(-2147418097)]
		short tabIndex { [DispId(-2147418097)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [TypeLibFunc(20)] [DispId(-2147418097)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x060141B4 RID: 82356
		[DispId(-2147416112)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void focus();

		// Token: 0x17006A6C RID: 27244
		// (get) Token: 0x060141B6 RID: 82358
		// (set) Token: 0x060141B5 RID: 82357
		[DispId(-2147416107)]
		string accessKey { [TypeLibFunc(20)] [DispId(-2147416107)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147416107)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A6D RID: 27245
		// (get) Token: 0x060141B8 RID: 82360
		// (set) Token: 0x060141B7 RID: 82359
		[DispId(-2147412097)]
		object onblur { [TypeLibFunc(20)] [DispId(-2147412097)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412097)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A6E RID: 27246
		// (get) Token: 0x060141BA RID: 82362
		// (set) Token: 0x060141B9 RID: 82361
		[DispId(-2147412098)]
		object onfocus { [TypeLibFunc(20)] [DispId(-2147412098)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412098)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A6F RID: 27247
		// (get) Token: 0x060141BC RID: 82364
		// (set) Token: 0x060141BB RID: 82363
		[DispId(-2147412076)]
		object onresize { [DispId(-2147412076)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412076)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x060141BD RID: 82365
		[DispId(-2147416110)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void blur();

		// Token: 0x060141BE RID: 82366
		[DispId(-2147416095)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void addFilter([MarshalAs(UnmanagedType.IUnknown)] [In] object pUnk);

		// Token: 0x060141BF RID: 82367
		[DispId(-2147416094)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void removeFilter([MarshalAs(UnmanagedType.IUnknown)] [In] object pUnk);

		// Token: 0x17006A70 RID: 27248
		// (get) Token: 0x060141C0 RID: 82368
		[DispId(-2147416093)]
		int clientHeight { [TypeLibFunc(20)] [DispId(-2147416093)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A71 RID: 27249
		// (get) Token: 0x060141C1 RID: 82369
		[DispId(-2147416092)]
		int clientWidth { [DispId(-2147416092)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A72 RID: 27250
		// (get) Token: 0x060141C2 RID: 82370
		[DispId(-2147416091)]
		int clientTop { [TypeLibFunc(20)] [DispId(-2147416091)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A73 RID: 27251
		// (get) Token: 0x060141C3 RID: 82371
		[DispId(-2147416090)]
		int clientLeft { [DispId(-2147416090)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x060141C4 RID: 82372
		[DispId(-2147417605)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool attachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

		// Token: 0x060141C5 RID: 82373
		[DispId(-2147417604)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void detachEvent([MarshalAs(UnmanagedType.BStr)] [In] string @event, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

		// Token: 0x17006A74 RID: 27252
		// (get) Token: 0x060141C6 RID: 82374
		[DispId(-2147412996)]
		object readyState { [DispId(-2147412996)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; }

		// Token: 0x17006A75 RID: 27253
		// (get) Token: 0x060141C8 RID: 82376
		// (set) Token: 0x060141C7 RID: 82375
		[DispId(-2147412087)]
		object onreadystatechange { [TypeLibFunc(20)] [DispId(-2147412087)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412087)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A76 RID: 27254
		// (get) Token: 0x060141CA RID: 82378
		// (set) Token: 0x060141C9 RID: 82377
		[DispId(-2147412050)]
		object onrowsdelete { [DispId(-2147412050)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412050)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A77 RID: 27255
		// (get) Token: 0x060141CC RID: 82380
		// (set) Token: 0x060141CB RID: 82379
		[DispId(-2147412049)]
		object onrowsinserted { [DispId(-2147412049)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412049)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A78 RID: 27256
		// (get) Token: 0x060141CE RID: 82382
		// (set) Token: 0x060141CD RID: 82381
		[DispId(-2147412048)]
		object oncellchange { [DispId(-2147412048)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412048)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A79 RID: 27257
		// (get) Token: 0x060141D0 RID: 82384
		// (set) Token: 0x060141CF RID: 82383
		[DispId(-2147412995)]
		string dir { [DispId(-2147412995)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147412995)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x060141D1 RID: 82385
		[DispId(-2147417056)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object createControlRange();

		// Token: 0x17006A7A RID: 27258
		// (get) Token: 0x060141D2 RID: 82386
		[DispId(-2147417055)]
		int scrollHeight { [TypeLibFunc(20)] [DispId(-2147417055)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A7B RID: 27259
		// (get) Token: 0x060141D3 RID: 82387
		[DispId(-2147417054)]
		int scrollWidth { [DispId(-2147417054)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A7C RID: 27260
		// (get) Token: 0x060141D5 RID: 82389
		// (set) Token: 0x060141D4 RID: 82388
		[DispId(-2147417053)]
		int scrollTop { [DispId(-2147417053)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [DispId(-2147417053)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x17006A7D RID: 27261
		// (get) Token: 0x060141D7 RID: 82391
		// (set) Token: 0x060141D6 RID: 82390
		[DispId(-2147417052)]
		int scrollLeft { [DispId(-2147417052)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [TypeLibFunc(20)] [DispId(-2147417052)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x060141D8 RID: 82392
		[DispId(-2147417050)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void clearAttributes();

		// Token: 0x17006A7E RID: 27262
		// (get) Token: 0x060141DA RID: 82394
		// (set) Token: 0x060141D9 RID: 82393
		[DispId(-2147412047)]
		object oncontextmenu { [DispId(-2147412047)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412047)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x060141DB RID: 82395
		[DispId(-2147417043)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement insertAdjacentElement([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement insertedElement);

		// Token: 0x060141DC RID: 82396
		[DispId(-2147417047)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElement applyElement([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement apply, [MarshalAs(UnmanagedType.BStr)] [In] string where);

		// Token: 0x060141DD RID: 82397
		[DispId(-2147417042)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string getAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where);

		// Token: 0x060141DE RID: 82398
		[DispId(-2147417041)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string replaceAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string newText);

		// Token: 0x17006A7F RID: 27263
		// (get) Token: 0x060141DF RID: 82399
		[DispId(-2147417040)]
		bool canHaveChildren { [DispId(-2147417040)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x060141E0 RID: 82400
		[DispId(-2147417032)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int addBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrUrl, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060141E1 RID: 82401
		[DispId(-2147417031)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool removeBehavior([In] int cookie);

		// Token: 0x17006A80 RID: 27264
		// (get) Token: 0x060141E2 RID: 82402
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle { [TypeLibFunc(1024)] [DispId(-2147417048)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006A81 RID: 27265
		// (get) Token: 0x060141E3 RID: 82403
		[DispId(-2147417030)]
		object behaviorUrns { [DispId(-2147417030)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006A82 RID: 27266
		// (get) Token: 0x060141E5 RID: 82405
		// (set) Token: 0x060141E4 RID: 82404
		[DispId(-2147417029)]
		string tagUrn { [DispId(-2147417029)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417029)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A83 RID: 27267
		// (get) Token: 0x060141E7 RID: 82407
		// (set) Token: 0x060141E6 RID: 82406
		[DispId(-2147412043)]
		object onbeforeeditfocus { [TypeLibFunc(20)] [DispId(-2147412043)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412043)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A84 RID: 27268
		// (get) Token: 0x060141E8 RID: 82408
		[DispId(-2147417028)]
		int readyStateValue { [DispId(-2147417028)] [TypeLibFunc(65)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x060141E9 RID: 82409
		[DispId(-2147417027)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(UnmanagedType.BStr)] [In] string v);

		// Token: 0x060141EA RID: 82410
		[DispId(-2147417016)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void mergeAttributes([MarshalAs(UnmanagedType.Interface)] [In] IHTMLElement mergeThis, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17006A85 RID: 27269
		// (get) Token: 0x060141EB RID: 82411
		[DispId(-2147417015)]
		bool isMultiLine { [DispId(-2147417015)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A86 RID: 27270
		// (get) Token: 0x060141EC RID: 82412
		[DispId(-2147417014)]
		bool canHaveHTML { [DispId(-2147417014)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A87 RID: 27271
		// (get) Token: 0x060141EE RID: 82414
		// (set) Token: 0x060141ED RID: 82413
		[DispId(-2147412039)]
		object onlayoutcomplete { [DispId(-2147412039)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412039)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A88 RID: 27272
		// (get) Token: 0x060141F0 RID: 82416
		// (set) Token: 0x060141EF RID: 82415
		[DispId(-2147412038)]
		object onpage { [DispId(-2147412038)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412038)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A89 RID: 27273
		// (get) Token: 0x060141F2 RID: 82418
		// (set) Token: 0x060141F1 RID: 82417
		[DispId(-2147417012)]
		bool inflateBlock { [DispId(-2147417012)] [TypeLibFunc(1089)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [TypeLibFunc(1089)] [DispId(-2147417012)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x17006A8A RID: 27274
		// (get) Token: 0x060141F4 RID: 82420
		// (set) Token: 0x060141F3 RID: 82419
		[DispId(-2147412035)]
		object onbeforedeactivate { [TypeLibFunc(20)] [DispId(-2147412035)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412035)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x060141F5 RID: 82421
		[DispId(-2147417011)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void setActive();

		// Token: 0x17006A8B RID: 27275
		// (get) Token: 0x060141F7 RID: 82423
		// (set) Token: 0x060141F6 RID: 82422
		[DispId(-2147412950)]
		string contentEditable { [DispId(-2147412950)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147412950)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006A8C RID: 27276
		// (get) Token: 0x060141F8 RID: 82424
		[DispId(-2147417010)]
		bool isContentEditable { [DispId(-2147417010)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A8D RID: 27277
		// (get) Token: 0x060141FA RID: 82426
		// (set) Token: 0x060141F9 RID: 82425
		[DispId(-2147412949)]
		bool hideFocus { [TypeLibFunc(20)] [DispId(-2147412949)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [DispId(-2147412949)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x17006A8E RID: 27278
		// (get) Token: 0x060141FC RID: 82428
		// (set) Token: 0x060141FB RID: 82427
		[DispId(-2147418036)]
		bool disabled { [TypeLibFunc(20)] [DispId(-2147418036)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; [DispId(-2147418036)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] set; }

		// Token: 0x17006A8F RID: 27279
		// (get) Token: 0x060141FD RID: 82429
		[DispId(-2147417007)]
		bool isDisabled { [DispId(-2147417007)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A90 RID: 27280
		// (get) Token: 0x060141FF RID: 82431
		// (set) Token: 0x060141FE RID: 82430
		[DispId(-2147412034)]
		object onmove { [TypeLibFunc(20)] [DispId(-2147412034)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412034)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A91 RID: 27281
		// (get) Token: 0x06014201 RID: 82433
		// (set) Token: 0x06014200 RID: 82432
		[DispId(-2147412033)]
		object oncontrolselect { [TypeLibFunc(20)] [DispId(-2147412033)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412033)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x06014202 RID: 82434
		[DispId(-2147417006)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool FireEvent([MarshalAs(UnmanagedType.BStr)] [In] string bstrEventName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006A92 RID: 27282
		// (get) Token: 0x06014204 RID: 82436
		// (set) Token: 0x06014203 RID: 82435
		[DispId(-2147412029)]
		object onresizestart { [DispId(-2147412029)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412029)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A93 RID: 27283
		// (get) Token: 0x06014206 RID: 82438
		// (set) Token: 0x06014205 RID: 82437
		[DispId(-2147412028)]
		object onresizeend { [TypeLibFunc(20)] [DispId(-2147412028)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412028)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A94 RID: 27284
		// (get) Token: 0x06014208 RID: 82440
		// (set) Token: 0x06014207 RID: 82439
		[DispId(-2147412031)]
		object onmovestart { [DispId(-2147412031)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412031)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A95 RID: 27285
		// (get) Token: 0x0601420A RID: 82442
		// (set) Token: 0x06014209 RID: 82441
		[DispId(-2147412030)]
		object onmoveend { [TypeLibFunc(20)] [DispId(-2147412030)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412030)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A96 RID: 27286
		// (get) Token: 0x0601420C RID: 82444
		// (set) Token: 0x0601420B RID: 82443
		[DispId(-2147412027)]
		object onmouseenter { [TypeLibFunc(20)] [DispId(-2147412027)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412027)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A97 RID: 27287
		// (get) Token: 0x0601420E RID: 82446
		// (set) Token: 0x0601420D RID: 82445
		[DispId(-2147412026)]
		object onmouseleave { [TypeLibFunc(20)] [DispId(-2147412026)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412026)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A98 RID: 27288
		// (get) Token: 0x06014210 RID: 82448
		// (set) Token: 0x0601420F RID: 82447
		[DispId(-2147412025)]
		object onactivate { [DispId(-2147412025)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412025)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A99 RID: 27289
		// (get) Token: 0x06014212 RID: 82450
		// (set) Token: 0x06014211 RID: 82449
		[DispId(-2147412024)]
		object ondeactivate { [DispId(-2147412024)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [TypeLibFunc(20)] [DispId(-2147412024)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x06014213 RID: 82451
		[DispId(-2147417005)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool dragDrop();

		// Token: 0x17006A9A RID: 27290
		// (get) Token: 0x06014214 RID: 82452
		[DispId(-2147417004)]
		int glyphMode { [DispId(-2147417004)] [TypeLibFunc(1089)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006A9B RID: 27291
		// (get) Token: 0x06014216 RID: 82454
		// (set) Token: 0x06014215 RID: 82453
		[DispId(-2147412036)]
		object onmousewheel { [DispId(-2147412036)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412036)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x06014217 RID: 82455
		[DispId(-2147417000)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		void normalize();

		// Token: 0x06014218 RID: 82456
		[DispId(-2147417003)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(UnmanagedType.BStr)] [In] string bstrName);

		// Token: 0x06014219 RID: 82457
		[DispId(-2147417002)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0601421A RID: 82458
		[DispId(-2147417001)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006A9C RID: 27292
		// (get) Token: 0x0601421C RID: 82460
		// (set) Token: 0x0601421B RID: 82459
		[DispId(-2147412022)]
		object onbeforeactivate { [DispId(-2147412022)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412022)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A9D RID: 27293
		// (get) Token: 0x0601421E RID: 82462
		// (set) Token: 0x0601421D RID: 82461
		[DispId(-2147412021)]
		object onfocusin { [TypeLibFunc(20)] [DispId(-2147412021)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412021)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A9E RID: 27294
		// (get) Token: 0x06014220 RID: 82464
		// (set) Token: 0x0601421F RID: 82463
		[DispId(-2147412020)]
		object onfocusout { [TypeLibFunc(20)] [DispId(-2147412020)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147412020)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006A9F RID: 27295
		// (get) Token: 0x06014221 RID: 82465
		[DispId(-2147417058)]
		int uniqueNumber { [TypeLibFunc(64)] [DispId(-2147417058)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006AA0 RID: 27296
		// (get) Token: 0x06014222 RID: 82466
		[DispId(-2147417057)]
		string uniqueID { [TypeLibFunc(64)] [DispId(-2147417057)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17006AA1 RID: 27297
		// (get) Token: 0x06014223 RID: 82467
		[DispId(-2147417066)]
		int nodeType { [DispId(-2147417066)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] get; }

		// Token: 0x17006AA2 RID: 27298
		// (get) Token: 0x06014224 RID: 82468
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode { [DispId(-2147417065)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x06014225 RID: 82469
		[DispId(-2147417064)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		bool hasChildNodes();

		// Token: 0x17006AA3 RID: 27299
		// (get) Token: 0x06014226 RID: 82470
		[DispId(-2147417063)]
		object childNodes { [DispId(-2147417063)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006AA4 RID: 27300
		// (get) Token: 0x06014227 RID: 82471
		[DispId(-2147417062)]
		object attributes { [DispId(-2147417062)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x06014228 RID: 82472
		[DispId(-2147417061)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode insertBefore([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object refChild);

		// Token: 0x06014229 RID: 82473
		[DispId(-2147417060)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode removeChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601422A RID: 82474
		[DispId(-2147417059)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode replaceChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601422B RID: 82475
		[DispId(-2147417051)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0601422C RID: 82476
		[DispId(-2147417046)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0601422D RID: 82477
		[DispId(-2147417044)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode swapNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601422E RID: 82478
		[DispId(-2147417045)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode replaceNode([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601422F RID: 82479
		[DispId(-2147417039)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IHTMLDOMNode appendChild([MarshalAs(UnmanagedType.Interface)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006AA5 RID: 27301
		// (get) Token: 0x06014230 RID: 82480
		[DispId(-2147417038)]
		string nodeName { [DispId(-2147417038)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17006AA6 RID: 27302
		// (get) Token: 0x06014232 RID: 82482
		// (set) Token: 0x06014231 RID: 82481
		[DispId(-2147417037)]
		object nodeValue { [DispId(-2147417037)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(-2147417037)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Struct)] set; }

		// Token: 0x17006AA7 RID: 27303
		// (get) Token: 0x06014233 RID: 82483
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild { [DispId(-2147417036)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006AA8 RID: 27304
		// (get) Token: 0x06014234 RID: 82484
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild { [DispId(-2147417035)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006AA9 RID: 27305
		// (get) Token: 0x06014235 RID: 82485
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling { [DispId(-2147417034)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006AAA RID: 27306
		// (get) Token: 0x06014236 RID: 82486
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling { [DispId(-2147417033)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17006AAB RID: 27307
		// (get) Token: 0x06014237 RID: 82487
		[DispId(-2147416999)]
		object ownerDocument { [DispId(-2147416999)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x17006AAC RID: 27308
		// (get) Token: 0x06014239 RID: 82489
		// (set) Token: 0x06014238 RID: 82488
		[DispId(-2147417091)]
		string dataFld { [DispId(-2147417091)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417091)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006AAD RID: 27309
		// (get) Token: 0x0601423B RID: 82491
		// (set) Token: 0x0601423A RID: 82490
		[DispId(-2147417090)]
		string dataSrc { [DispId(-2147417090)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417090)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006AAE RID: 27310
		// (get) Token: 0x0601423D RID: 82493
		// (set) Token: 0x0601423C RID: 82492
		[DispId(-2147417089)]
		string dataFormatAs { [DispId(-2147417089)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(-2147417089)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x17006AAF RID: 27311
		// (get) Token: 0x0601423F RID: 82495
		// (set) Token: 0x0601423E RID: 82494
		[DispId(-2147418039)]
		string align { [DispId(-2147418039)] [TypeLibFunc(20)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [TypeLibFunc(20)] [DispId(-2147418039)] [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }
	}
}
