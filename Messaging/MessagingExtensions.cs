﻿using System;

namespace SolarOdyssey.Messaging
{
    public static class MessagingExtensions
    {
        private static readonly MessageHub hub = new MessageHub();


        public static bool Exists<T>( this object obj ) => hub.Exists<T>( obj );

        public static void Publish<T>( this object obj ) => hub.Publish( obj, default( T ) );

        public static void Publish<T>( this object obj, T data ) => hub.Publish( obj, data );

        public static void Subscribe<T>( this object obj, Action<T> handler ) => hub.Subscribe( obj, handler );

        public static void Unsubscribe( this object obj ) => hub.Unsubscribe( obj );

        public static void Unsubscribe<T>( this object obj ) => hub.Unsubscribe( obj, (Action<T>) null );

        public static void Unsubscribe<T>( this object obj, Action<T> handler ) => hub.Unsubscribe( obj, handler );
    }
}