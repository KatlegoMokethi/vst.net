﻿namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System.Linq;

    /// <summary>
    /// Forwards the <see cref="IVstMidiProcessor"/> calls to the host stub.
    /// </summary>
    internal sealed class VstHostMidiProcessor : IVstMidiProcessor
    {
        private readonly VstHost _host;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostMidiProcessor(VstHost host)
        {
            Throw.IfArgumentIsNull(host, nameof(host));

            _host = host;
        }

        #region IVstMidiProcessor Members

        /// <summary>
        /// Always returns 16.
        /// </summary>
        public int ChannelCount
        {
            // default number of channels for host
            get { return 16; }
        }

        /// <summary>
        /// Passes the <paramref name="events"/> onto the host.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="events"/> is not set to an instance of an object.</exception>
        public void Process(VstEventCollection events)
        {
            Throw.IfArgumentIsNull(events, nameof(events));

            _host.HostCommandProxy.Commands.ProcessEvents(events.ToArray());
        }

        #endregion
    }
}
