using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Uhf.Api.Cls {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='GpiTriggerBoundaryListener']"
	[Register ("com/uhf/api/cls/GpiTriggerBoundaryListener", "", "Com.Uhf.Api.Cls.IGpiTriggerBoundaryListenerInvoker")]
	public partial interface IGpiTriggerBoundaryListener : IJavaObject, IJavaPeerable {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='GpiTriggerBoundaryListener']/method[@name='GpiTriggerBoundary' and count(parameter)=3 and parameter[1][@type='com.uhf.api.cls.Reader'] and parameter[2][@type='com.uhf.api.cls.GpiTriggerBoundaryType'] and parameter[3][@type='com.uhf.api.cls.GpiTriggerBoundaryReasonType']]"
		[Register ("GpiTriggerBoundary", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/GpiTriggerBoundaryType;Lcom/uhf/api/cls/GpiTriggerBoundaryReasonType;)V", "GetGpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_Handler:Com.Uhf.Api.Cls.IGpiTriggerBoundaryListenerInvoker, BindingLib")]
		void GpiTriggerBoundary (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType p1, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType p2);

	}

	[global::Android.Runtime.Register ("com/uhf/api/cls/GpiTriggerBoundaryListener", DoNotGenerateAcw=true)]
	internal partial class IGpiTriggerBoundaryListenerInvoker : global::Java.Lang.Object, IGpiTriggerBoundaryListener {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/uhf/api/cls/GpiTriggerBoundaryListener", typeof (IGpiTriggerBoundaryListenerInvoker));

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

		public static IGpiTriggerBoundaryListener GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IGpiTriggerBoundaryListener> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.uhf.api.cls.GpiTriggerBoundaryListener'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IGpiTriggerBoundaryListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_;
#pragma warning disable 0169
		static Delegate GetGpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_Handler ()
		{
			if (cb_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_ == null)
				cb_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPLLL_V (n_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_));
			return cb_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_;
		}

		static void n_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.IGpiTriggerBoundaryListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.Reader> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType> (native_p1, JniHandleOwnership.DoNotTransfer);
			var p2 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType> (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.GpiTriggerBoundary (p0, p1, p2);
		}
#pragma warning restore 0169

		IntPtr id_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_;
		public unsafe void GpiTriggerBoundary (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType p1, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType p2)
		{
			if (id_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_ == IntPtr.Zero)
				id_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_ = JNIEnv.GetMethodID (class_ref, "GpiTriggerBoundary", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/GpiTriggerBoundaryType;Lcom/uhf/api/cls/GpiTriggerBoundaryReasonType;)V");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_GpiTriggerBoundary_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiTriggerBoundaryType_Lcom_uhf_api_cls_GpiTriggerBoundaryReasonType_, __args);
		}

	}

	// event args for com.uhf.api.cls.GpiTriggerBoundaryListener.GpiTriggerBoundary
	public partial class GpiTriggerBoundaryEventArgs : global::System.EventArgs {
		public GpiTriggerBoundaryEventArgs (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType p1, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType p2)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
		}

		global::Com.Uhf.Api.Cls.Reader p0;

		public global::Com.Uhf.Api.Cls.Reader P0 {
			get { return p0; }
		}

		global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType p1;

		public global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType P1 {
			get { return p1; }
		}

		global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType p2;

		public global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType P2 {
			get { return p2; }
		}

	}

	[global::Android.Runtime.Register ("mono/com/uhf/api/cls/GpiTriggerBoundaryListenerImplementor")]
	internal sealed partial class IGpiTriggerBoundaryListenerImplementor : global::Java.Lang.Object, IGpiTriggerBoundaryListener {

		object sender;

		public IGpiTriggerBoundaryListenerImplementor (object sender) : base (global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/uhf/api/cls/GpiTriggerBoundaryListenerImplementor", "()V"), JniHandleOwnership.TransferLocalRef)
		{
			global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
			this.sender = sender;
		}

		#pragma warning disable 0649
		public EventHandler<GpiTriggerBoundaryEventArgs> Handler;
		#pragma warning restore 0649

		public void GpiTriggerBoundary (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryType p1, global::Com.Uhf.Api.Cls.GpiTriggerBoundaryReasonType p2)
		{
			var __h = Handler;
			if (__h != null)
				__h (sender, new GpiTriggerBoundaryEventArgs (p0, p1, p2));
		}

		internal static bool __IsEmpty (IGpiTriggerBoundaryListenerImplementor value)
		{
			return value.Handler == null;
		}

	}
}
