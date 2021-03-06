﻿namespace Rollbar.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Rollbar.Diagnostics;

    /// <summary>
    /// IMplements Telemetry DTO
    /// </summary>
    /// <seealso cref="Rollbar.DTOs.ExtendableDtoBase" />
    public class Telemetry
        : ExtendableDtoBase
    {
        /// <summary>
        /// The DTO reserved properties.
        /// </summary>
        public static class ReservedProperties
        {
            public const string Level = "level";
            public const string Type = "type";
            public const string Source = "source";
            public const string Timestamp = "timestamp_ms";
            public const string Body = "body";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Telemetry"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="level">The level.</param>
        /// <param name="body">The body.</param>
        /// <param name="arbitraryKeyValuePairs">The arbitrary key value pairs.</param>
        public Telemetry(
            TelemetrySource source
            ,TelemetryLevel level
            ,TelemetryBody body
            ,IDictionary<string, object> arbitraryKeyValuePairs = null
            )
            : base(arbitraryKeyValuePairs)
        {
            Assumption.AssertNotNull(body, nameof(body));

            this.Timestamp = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            this.Source = source;
            this.Level = level;
            this.Type = body.Type;
            this.Body = body;

            Validate();
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public TelemetryLevel Level
        {
            get { return (TelemetryLevel) this._keyedValues[ReservedProperties.Level]; }
            private set { this._keyedValues[ReservedProperties.Level] = value; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TelemetryType Type
        {
            get { return (TelemetryType) this._keyedValues[ReservedProperties.Type]; }
            private set { this._keyedValues[ReservedProperties.Type] = value; }
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public TelemetrySource Source
        {
            get { return (TelemetrySource)this._keyedValues[ReservedProperties.Source]; }
            private set { this._keyedValues[ReservedProperties.Source] = value; }
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public long Timestamp
        {
            get { return (long) this._keyedValues[ReservedProperties.Timestamp]; }
            private set { this._keyedValues[ReservedProperties.Timestamp] = value; }
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public TelemetryBody Body
        {
            get { return this._keyedValues[ReservedProperties.Body] as TelemetryBody; }
            private set { this._keyedValues[ReservedProperties.Body] = value; }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            Assumption.AssertNotNull(this.Body, nameof(this.Body));
            Assumption.AssertTrue(this.Body.GetType().Name.Contains(this.Type.ToString()), nameof(this.Body));
        }
    }
}
