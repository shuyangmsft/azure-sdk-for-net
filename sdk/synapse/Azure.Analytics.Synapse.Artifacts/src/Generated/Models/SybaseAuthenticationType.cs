// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.Analytics.Synapse.Artifacts.Models
{
    /// <summary> AuthenticationType to be used for connection. </summary>
    public readonly partial struct SybaseAuthenticationType : IEquatable<SybaseAuthenticationType>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="SybaseAuthenticationType"/> values are the same. </summary>
        public SybaseAuthenticationType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string BasicValue = "Basic";
        private const string WindowsValue = "Windows";

        /// <summary> Basic. </summary>
        public static SybaseAuthenticationType Basic { get; } = new SybaseAuthenticationType(BasicValue);
        /// <summary> Windows. </summary>
        public static SybaseAuthenticationType Windows { get; } = new SybaseAuthenticationType(WindowsValue);
        /// <summary> Determines if two <see cref="SybaseAuthenticationType"/> values are the same. </summary>
        public static bool operator ==(SybaseAuthenticationType left, SybaseAuthenticationType right) => left.Equals(right);
        /// <summary> Determines if two <see cref="SybaseAuthenticationType"/> values are not the same. </summary>
        public static bool operator !=(SybaseAuthenticationType left, SybaseAuthenticationType right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="SybaseAuthenticationType"/>. </summary>
        public static implicit operator SybaseAuthenticationType(string value) => new SybaseAuthenticationType(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is SybaseAuthenticationType other && Equals(other);
        /// <inheritdoc />
        public bool Equals(SybaseAuthenticationType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}