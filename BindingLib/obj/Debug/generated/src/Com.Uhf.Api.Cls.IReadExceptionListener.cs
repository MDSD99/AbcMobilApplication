using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Uhf.Api.Cls {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='ReadExceptionListener']"
	[Register ("com/uhf/api/cls/ReadExceptionListener", "", "Com.Uhf.Api.Cls.IReadExceptionListenerInvoker")]
	public partial interface IReadExceptionListener : IJavaObject, IJavaPeerable {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.uhf.api.cls']/interface[@name='ReadExceptionListener']/method[@name='tagReadException' and count(parameter)=2 and parameter[1][@type='com.uhf.api.cls.Reader'] and parameter[2][@type='com.uhf.api.cls.Reader.READER_ERR']]"
		[Register ("tagReadException", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/Reader$READER_ERR;)V", "GetTagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_Handler:Com.Uhf.Api.Cls.IReadExceptionListenerInvoker, BindingLib")]
		void TagReadException (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.READER_ERR p1);

	}

	[global::Android.Runtime.Register ("com/uhf/api/cls/ReadExceptionListener", DoNotGenerateAcw=true)]
	internal partial class IReadExceptionListenerInvoker : global::Java.Lang.Object, IReadExceptionListener {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/uhf/api/cls/ReadExceptionListener", typeof (IReadExceptionListenerInvoker));

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

		public static IReadExceptionListener GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IReadExceptionListener> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.uhf.api.cls.ReadExceptionListener'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IReadExceptionListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_;
#pragma warning disable 0169
		static Delegate GetTagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_Handler ()
		{
			if (cb_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_ == null)
				cb_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPLL_V (n_TagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_));
			return cb_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_;
		}

		static void n_TagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.IReadExceptionListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			var p0 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.Reader> (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Java.Lang.Object.GetObject<global::Com.Uhf.Api.Cls.Reader.READER_ERR> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.TagReadException (p0, p1);
		}
#pragma warning restore 0169

		IntPtr id_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_;
		public unsafe void TagReadException (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.READER_ERR p1)
		{
			if (id_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_ == IntPtr.Zero)
				id_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_ = JNIEnv.GetMethodID (class_ref, "tagReadException", "(Lcom/uhf/api/cls/Reader;Lcom/uhf/api/cls/Reader$READER_ERR;)V");
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_tagReadException_Lcom_uhf_api_cls_Reader_Lcom_uhf_api_cls_Reader_READER_ERR_, __args);
		}

	}

	// event args for com.uhf.api.cls.ReadExceptionListener.tagReadException
	public partial class ReadExceptionEventArgs : global::System.EventArgs {
		public ReadExceptionEventArgs (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.READER_ERR p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		global::Com.Uhf.Api.Cls.Reader p0;

		public global::Com.Uhf.Api.Cls.Reader P0 {
			get { return p0; }
		}

		global::Com.Uhf.Api.Cls.Reader.READER_ERR p1;

		public global::Com.Uhf.Api.Cls.Reader.READER_ERR P1 {
			get { return p1; }
		}

	}

	[global::Android.Runtime.Register ("mono/com/uhf/api/cls/ReadExceptionListenerImplementor")]
	internal sealed partial class IReadExceptionListenerImplementor : global::Java.Lang.Object, IReadExceptionListener {

		object sender;

		public IReadExceptionListenerImplementor (object sender) : base (global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/uhf/api/cls/ReadExceptionListenerImplementor", "()V"), JniHandleOwnership.TransferLocalRef)
		{
			global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
			this.sender = sender;
		}

		#pragma warning disable 0649
		public EventHandler<ReadExceptionEventArgs> Handler;
		#pragma warning restore 0649

		public void TagReadException (global::Com.Uhf.Api.Cls.Reader p0, global::Com.Uhf.Api.Cls.Reader.READER_ERR p1)
		{
			var __h = Handler;
			if (__h != null)
				__h (sender, new ReadExceptionEventArgs (p0, p1));
		}

		internal static bool __IsEmpty (IReadExceptionListenerImplementor value)
		{
			return value.Handler == null;
		}

	}
}
