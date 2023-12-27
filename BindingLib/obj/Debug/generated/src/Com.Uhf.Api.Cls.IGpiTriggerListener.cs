using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Uhf.Api.Cls {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='GpiTriggerListener']"
	[Register ("com/uhf/api/cls/GpiTriggerListener", "", "Com.Uhf.Api.Cls.IGpiTriggerListenerInvoker")]
	public partial interface IGpiTriggerListener : IJavaObject, IJavaPeerable {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='GpiTriggerListener']/method[@name='GpiTrigger' and count(parameter)=3 and parameter[1][@type='com.uhf.api.cls.Reader'] and parameter[2][@type='com.uhf.api.cls.GpiInfo_ST'] and parameter[3][@type='int']]"
		[Register ("GpiTrigger", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/GpiInfo_ST;I)V", "GetGpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_IHandler:Com.Uhf.Api.Cls.IGpiTriggerListenerInvoker, BindingLib")]
		void GpiTrigger (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiInfo_ST p1, int p2);

	}

	[global::Android.Runtime.Register ("com/uhf/api/cls/GpiTriggerListener", DoNotGenerateAcw=true)]
	internal partial class IGpiTriggerListenerInvoker : global::Java.Lang.Object, IGpiTriggerListener {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/uhf/api/cls/GpiTriggerListener", typeof (IGpiTriggerListenerInvoker));

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

		public static IGpiTriggerListener GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IGpiTriggerListener> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.uhf.api.cls.GpiTriggerListener'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IGpiTriggerListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I;
#pragma warning disable 0169
		static Delegate GetGpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_IHandler ()
		{
			if (cb_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I == null)
				cb_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPLLI_V (n_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I));
			return cb_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I;
		}

		static void n_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, int p2)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.IGpiTriggerListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.Reader> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.GpiInfo_ST> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.GpiTrigger (p0, p1, p2);
		}
#pragma warning restore 0169

		IntPtr id_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I;
		public unsafe void GpiTrigger (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiInfo_ST p1, int p2)
		{
			if (id_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I == IntPtr.Zero)
				id_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I = JNIEnv.GetMethodID (class_ref, "GpiTrigger", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/GpiInfo_ST;I)V");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue (p2);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_GpiTrigger_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_GpiInfo_ST_I, __args);
		}

	}

	// event args for com.uhf.api.cls.GpiTriggerListener.GpiTrigger
	public partial class GpiTriggerEventArgs : global::System.EventArgs {
		public GpiTriggerEventArgs (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiInfo_ST p1, int p2)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
		}

		global::Com.Uhf.Api.Cls.Reader p0;

		public global::Com.Uhf.Api.Cls.Reader P0 {
			get { return p0; }
		}

		global::Com.Uhf.Api.Cls.GpiInfo_ST p1;

		public global::Com.Uhf.Api.Cls.GpiInfo_ST P1 {
			get { return p1; }
		}

		int p2;

		public int P2 {
			get { return p2; }
		}

	}

	[global::Android.Runtime.Register ("mono/com/uhf/api/cls/GpiTriggerListenerImplementor")]
	internal sealed partial class IGpiTriggerListenerImplementor : global::Java.Lang.Object, IGpiTriggerListener {

		object sender;

		public IGpiTriggerListenerImplementor (object sender) : base (global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/uhf/api/cls/GpiTriggerListenerImplementor", "()V"), JniHandleOwnership.TransferLocalRef)
		{
			global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
			this.sender = sender;
		}

		#pragma warning disable 0649
		public EventHandler<GpiTriggerEventArgs> Handler;
		#pragma warning restore 0649

		public void GpiTrigger (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.GpiInfo_ST p1, int p2)
		{
			var __h = Handler;
			if (__h != null)
				__h (sender, new GpiTriggerEventArgs (p0, p1, p2));
		}

		internal static bool __IsEmpty (IGpiTriggerListenerImplementor value)
		{
			return value.Handler == null;
		}

	}
}
