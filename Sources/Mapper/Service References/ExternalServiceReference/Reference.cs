﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mapper.ExternalServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProcessRequest", Namespace="http://schemas.datacontract.org/2004/07/ExternalService")]
    [System.SerializableAttribute()]
    public partial class ProcessRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperActivatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperConnectedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperPostDequeuedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperPreDequeuedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperReceivedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperSentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PayloadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ProducedField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperActivated {
            get {
                return this.MapperActivatedField;
            }
            set {
                if ((this.MapperActivatedField.Equals(value) != true)) {
                    this.MapperActivatedField = value;
                    this.RaisePropertyChanged("MapperActivated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperConnected {
            get {
                return this.MapperConnectedField;
            }
            set {
                if ((this.MapperConnectedField.Equals(value) != true)) {
                    this.MapperConnectedField = value;
                    this.RaisePropertyChanged("MapperConnected");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperPostDequeued {
            get {
                return this.MapperPostDequeuedField;
            }
            set {
                if ((this.MapperPostDequeuedField.Equals(value) != true)) {
                    this.MapperPostDequeuedField = value;
                    this.RaisePropertyChanged("MapperPostDequeued");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperPreDequeued {
            get {
                return this.MapperPreDequeuedField;
            }
            set {
                if ((this.MapperPreDequeuedField.Equals(value) != true)) {
                    this.MapperPreDequeuedField = value;
                    this.RaisePropertyChanged("MapperPreDequeued");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperReceived {
            get {
                return this.MapperReceivedField;
            }
            set {
                if ((this.MapperReceivedField.Equals(value) != true)) {
                    this.MapperReceivedField = value;
                    this.RaisePropertyChanged("MapperReceived");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperSent {
            get {
                return this.MapperSentField;
            }
            set {
                if ((this.MapperSentField.Equals(value) != true)) {
                    this.MapperSentField = value;
                    this.RaisePropertyChanged("MapperSent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Payload {
            get {
                return this.PayloadField;
            }
            set {
                if ((object.ReferenceEquals(this.PayloadField, value) != true)) {
                    this.PayloadField = value;
                    this.RaisePropertyChanged("Payload");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Produced {
            get {
                return this.ProducedField;
            }
            set {
                if ((this.ProducedField.Equals(value) != true)) {
                    this.ProducedField = value;
                    this.RaisePropertyChanged("Produced");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProcessResponse", Namespace="http://schemas.datacontract.org/2004/07/ExternalService")]
    [System.SerializableAttribute()]
    public partial class ProcessResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperActivatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperConnectedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperPostDequeuedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperPreDequeuedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperReceivedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long MapperSentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PayloadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ProducedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ServiceReceivedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ServiceSentField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperActivated {
            get {
                return this.MapperActivatedField;
            }
            set {
                if ((this.MapperActivatedField.Equals(value) != true)) {
                    this.MapperActivatedField = value;
                    this.RaisePropertyChanged("MapperActivated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperConnected {
            get {
                return this.MapperConnectedField;
            }
            set {
                if ((this.MapperConnectedField.Equals(value) != true)) {
                    this.MapperConnectedField = value;
                    this.RaisePropertyChanged("MapperConnected");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperPostDequeued {
            get {
                return this.MapperPostDequeuedField;
            }
            set {
                if ((this.MapperPostDequeuedField.Equals(value) != true)) {
                    this.MapperPostDequeuedField = value;
                    this.RaisePropertyChanged("MapperPostDequeued");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperPreDequeued {
            get {
                return this.MapperPreDequeuedField;
            }
            set {
                if ((this.MapperPreDequeuedField.Equals(value) != true)) {
                    this.MapperPreDequeuedField = value;
                    this.RaisePropertyChanged("MapperPreDequeued");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperReceived {
            get {
                return this.MapperReceivedField;
            }
            set {
                if ((this.MapperReceivedField.Equals(value) != true)) {
                    this.MapperReceivedField = value;
                    this.RaisePropertyChanged("MapperReceived");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long MapperSent {
            get {
                return this.MapperSentField;
            }
            set {
                if ((this.MapperSentField.Equals(value) != true)) {
                    this.MapperSentField = value;
                    this.RaisePropertyChanged("MapperSent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Payload {
            get {
                return this.PayloadField;
            }
            set {
                if ((object.ReferenceEquals(this.PayloadField, value) != true)) {
                    this.PayloadField = value;
                    this.RaisePropertyChanged("Payload");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Produced {
            get {
                return this.ProducedField;
            }
            set {
                if ((this.ProducedField.Equals(value) != true)) {
                    this.ProducedField = value;
                    this.RaisePropertyChanged("Produced");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ServiceReceived {
            get {
                return this.ServiceReceivedField;
            }
            set {
                if ((this.ServiceReceivedField.Equals(value) != true)) {
                    this.ServiceReceivedField = value;
                    this.RaisePropertyChanged("ServiceReceived");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ServiceSent {
            get {
                return this.ServiceSentField;
            }
            set {
                if ((this.ServiceSentField.Equals(value) != true)) {
                    this.ServiceSentField = value;
                    this.RaisePropertyChanged("ServiceSent");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ExternalServiceReference.IExternalService")]
    public interface IExternalService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExternalService/Process", ReplyAction="http://tempuri.org/IExternalService/ProcessResponse")]
        Mapper.ExternalServiceReference.ProcessResponse Process(Mapper.ExternalServiceReference.ProcessRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExternalServiceChannel : Mapper.ExternalServiceReference.IExternalService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExternalServiceClient : System.ServiceModel.ClientBase<Mapper.ExternalServiceReference.IExternalService>, Mapper.ExternalServiceReference.IExternalService {
        
        public ExternalServiceClient() {
        }
        
        public ExternalServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExternalServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExternalServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExternalServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Mapper.ExternalServiceReference.ProcessResponse Process(Mapper.ExternalServiceReference.ProcessRequest request) {
            return base.Channel.Process(request);
        }
    }
}
