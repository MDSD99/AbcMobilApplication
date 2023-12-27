using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Uhf.Api.Cls {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='ReadListener']"
	[Register ("com/uhf/api/cls/ReadListener", "", "Com.Uhf.Api.Cls.IReadListenerInvoker")]
	public partial interface IReadListener : IJavaObject, IJavaPeerable {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='ReadListener']/method[@name='tagRead' and count(parameter)=2 and parameter[1][@type='com.uhf.api.cls.Reader'] and parameter[2][@type='com.uhf.api.cls.Reader.TAGINFO[]']]"
		[Register ("tagRead", "(Lcom/uhf/api/cls/Reader;[Lcom/uhf/api/cls/Reader$TAGINFO;)V", "GetTagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_Handler:Com.Uhf.Api.Cls.IReadListenerInvoker, BindingLib")]
		void TagRead (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.TAGINFO[] p1);

	}

	[global::Android.Runtime.Register ("com/uhf/api/cls/ReadListener", DoNotGenerateAcw=true)]
	internal partial class IReadListenerInvoker : global::Java.Lang.Object, IReadListener {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/uhf/api/cls/ReadListener", typeof (IReadListenerInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IReadListener GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IReadListener> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.uhf.api.cls.ReadListener'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IReadListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_;
#pragma warning disable 0169
		static Delegate GetTagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_Handler ()
		{
			if (cb_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_ == null)
				cb_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPLL_V (n_TagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_));
			return cb_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_;
		}

		static void n_TagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.IReadListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.Reader> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = (global::Com.Uhf.Api.Cls.Reader.TAGINFO[]) JNIEnv.GetArray (native_p1, JniHandleOwnership.DoNotTransfer, typeof (global::Com.Uhf.Api.Cls.Reader.TAGINFO));
			__this.TagRead (p0, p1);
			if (p1 != null)
				JNIEnv.CopyArray (p1, native_p1);
		}
#pragma warning restore 0169

		IntPtr id_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_;
		public unsafe void TagRead (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.TAGINFO[] p1)
		{
			if (id_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_ == IntPtr.Zero)
				id_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_ = JNIEnv.GetMethodID (class_ref, "tagRead", "(Lcom/uhf/api/cls/Reader;[Lcom/uhf/api/cls/Reader$TAGINFO;)V");
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue (native_p1);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_tagRead_Lcom_uhf_api_cls_Reader_arrayLcom_uhf_api_cls_Reader_TAGINFO_, __args);
			if (p1 != null) {
				JNIEnv.CopyArray (native_p1, p1);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

	}

	// event args for com.uhf.api.cls.ReadListener.tagRead
	public partial class ReadEventArgs : global::System.EventArgs {
		public ReadEventArgs (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.TAGINFO[] p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		global::Com.Uhf.Api.Cls.Reader p0;

		public global::Com.Uhf.Api.Cls.Reader P0 {
			get { return p0; }
		}

		global::Com.Uhf.Api.Cls.Reader.TAGINFO[] p1;

		public global::Com.Uhf.Api.Cls.Reader.TAGINFO[] P1 {
			get { return p1; }
		}

	}

	[global::Android.Runtime.Register ("mono/com/uhf/api/cls/ReadListenerImplementor")]
	internal sealed partial class IReadListenerImplementor : global::Java.Lang.Object, IReadListener {

		object sender;

		public IReadListenerImplementor (object sender) : base (global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/uhf/api/cls/ReadListenerImplementor", "()V"), JniHandleOwnership.TransferLocalRef)
		{
			global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
			this.sender = sender;
		}

		#pragma warning disable 0649
		public EventHandler<ReadEventArgs> Handler;
		#pragma warning restore 0649

		public void TagRead (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.TAGINFO[] p1)
		{
			var __h = Handler;
			if (__h != null)
				__h (sender, new ReadEventArgs (p0, p1));
		}

		internal static bool __IsEmpty (IReadListenerImplementor value)
		{
			return value.Handler == null;
		}

	}
}
