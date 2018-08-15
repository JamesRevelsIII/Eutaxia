using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eutaxia
{
    public class Mapping<T1, T2>
    {
        public Dictionary<T1, List<T2>> map;

        public Mapping()
        {
            map = new Dictionary<T1, List<T2>>();
        }

        public void AddToMap(T1 _key, T2 _value)
        {
            if (map.ContainsKey(_key))
            {
                map[_key].Add(_value);
            }
            else
            {
                List<T2> placeHolder = new List<T2>();
                placeHolder.Add(_value);
                map.Add(_key, placeHolder);
            }
        }

        public List<T2> FindValue(T1 _key)
        {
            return map[_key];
        }


    }
    public class Quadramapping<T1, T2, T3, T4>
    {
        //Holds 4 Mappings of Different Types
        public Mapping<float[], T1> map1;
        public Mapping<float[], T2> map2;
        public Mapping<float[], T3> map3;
        public Mapping<float[], T4> map4;

        public Quadramapping()
        {
            map1 = new Mapping<float[], T1>();
            map2 = new Mapping<float[], T2>();
            map3 = new Mapping<float[], T3>();
            map4 = new Mapping<float[], T4>();
        }
    }
    public class Octomapping<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        //Holds 8 Mappings of Different Types
        public Mapping<float[], T1> map1;
        public Mapping<float[], T2> map2;
        public Mapping<float[], T3> map3;
        public Mapping<float[], T4> map4;
        public Mapping<float[], T5> map5;
        public Mapping<float[], T6> map6;
        public Mapping<float[], T7> map7;
        public Mapping<float[], T8> map8;

        public Octomapping()
        {
            map1 = new Mapping<float[], T1>();
            map2 = new Mapping<float[], T2>();
            map3 = new Mapping<float[], T3>();
            map4 = new Mapping<float[], T4>();
            map5 = new Mapping<float[], T5>();
            map6 = new Mapping<float[], T6>();
            map7 = new Mapping<float[], T7>();
            map8 = new Mapping<float[], T8>();
        }


    }

    public interface IPoint
    {
        void SetCoordinates(float[] _coordinates);
        float[] GetCoordinates();

    }
    public interface IThing: IPoint
    {
        float[] GetPosition();
        float[] GetPhase();
        float[] GetExtra();
    }
    public interface IRecursiveThing : IThing
    {
        void Iterate();
        void Recur(int _frequency); //Does multiple Iterations 
    }

    [Serializable]
    public class Thing : IRecursiveThing
    {
        public string name;
        public float[] coordinates;

        public Thing()
        {
            coordinates = new float[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            name = "";
        }

        public void SetCoordinates(float[] _coordinates)
        {
            coordinates = _coordinates;
        }
        public float[] GetCoordinates()
        {
            return coordinates;
        }
        public void ToMapping(Mapping<float[], Thing> _target)
        {
            _target.AddToMap(coordinates, this);
        }
        public void ToMapping(Mapping<float[], Thing> _target, List<Thing> _things)
        {
            foreach(Thing T in _things)
            {
                T.ToMapping(_target);
            }
        }

        public float[] GetPosition()
        {
            float[] output = new float[3];
            output[0] = coordinates[0];
            output[1] = coordinates[1];
            output[2] = coordinates[2];
            return output;
        }
        public float[] GetPhase()
        {
            float[] output = new float[3];
            output[0] = coordinates[3];
            output[1] = coordinates[4];
            output[2] = coordinates[5];
            return output;
        }
        public float[] GetExtra()
        {
            float[] output = new float[3];
            output[0] = coordinates[6];
            output[1] = coordinates[7];
            output[2] = coordinates[8];
            return output;
        }

        public void SetPosition(float _x,float _y, float _z)
        {
            coordinates[0] = _x;
            coordinates[1] = _y;
            coordinates[2] = _z;
        }
        public void SetPosition(float[] _position)
        {
            coordinates[0] = _position[0];
            coordinates[1] = _position[1];
            coordinates[2] = _position[2];

        }

        public void SetPhase(float _x,float _y, float _z)
        {
            coordinates[3] = _x;
            coordinates[4] = _y;
            coordinates[5] = _z;
        }
        public void SetPhase(float[] _phase)
        {
            coordinates[3] = _phase[0];
            coordinates[4] = _phase[1];
            coordinates[5] = _phase[2];

        }

        public void SetExtra(float _x,float _y, float _z)
        {
            coordinates[6] = _x;
            coordinates[7] = _y;
            coordinates[8] = _z;
        }
        public void SetExtra(float[] _position)
        {
            coordinates[6] = _position[0];
            coordinates[7] = _position[1];
            coordinates[8] = _position[2];

        }

        public void IncrementDimension(int _dimension, float _increment)
        {
            coordinates[_dimension] = coordinates[_dimension] + _increment;
        }
        public float FindDistance(Thing _thing1, Thing _thing2)
        {
            int counter = 0;
            float sum = 0f;
            float square = 0f;
            if (_thing1.GetCoordinates().Length == _thing2.GetCoordinates().Length)
            {
                for (counter = 0; counter < _thing1.coordinates.Length; counter++)
                {
                    square = Math.Abs(_thing1.GetCoordinates()[counter] - _thing2.GetCoordinates()[counter]);
                    sum += square;
                }
            }
            else
            {
                Debug.Log("Thing Coordinates are unequal!");
                return 0f;
            }
            return (float)Math.Sqrt(sum);
        }

        //IRecursion
        public void Iterate()
        {

        }
        public void Recur(int _frequency)
        {
            int counter = 0;
            for (counter = 0; counter < _frequency; counter++)
            {
                Iterate();
            }
        }


    }
    
    namespace DesignPatterns
    {
        public class Singleton : Thing
        {
            // A class with a single instantiate used to Manage Data,etc.
            public Singleton()
            {

            }
        }

        public class Factory
        {
            //A class that is used to build object quick i.e. Shape Factory

            public Factory()
            {

            }
        }

        public class AbstractFactory
        {
            //A class that creates families of objects
            //Encapsulates a group of individual Factory with common theme
            public List<Factory> factories;

            public AbstractFactory()
            {

            }
        }

        public class Recipe<T1,T2>
        {
            public T1 result;
            public List<T2> ingredients;

            private class Part<T2>
            {
                T2 type;
            }

            public void CookIngredients()
            {
                foreach (T2 i in ingredients)
                {

                }
        
            }

        }
        public class Builder<T>

        {
            //builds a complex objects
            public T result;

            public void Build()
            {
               
            }
        }
    }
    namespace ClientServerModel
    {
        public interface IClient
        {
            void SendRequest(Server _server);
            Request GetRequest();
            void SetRequest(Request _request);
        }
        public abstract class Client : IClient
        {
            private Request request;

            public virtual void SendRequest(Server _Server)
            {
                
            }

            public Request GetRequest()
            {
                return request;
            }
            public void SetRequest(Request _request)
            {
                request = _request;
            }
        }

        public interface IServer
        {
            void HandleRequest(Request _request);
            void HandleRequestQueue();
            Queue<Request> GetRequestQueue();

        }
        public abstract class Server : IServer
        {
            private Queue<Request> requestQueue
            {
                get { return RequestQueue; }
            }

            public Server()
            {
               
            }

            public virtual void HandleRequest(Request _request)
            {
                
            }
            public virtual void HandleRequestQueue()
            {

            }

            public Queue<Request> GetRequestQueue()
            {
                return requestQueue;
            }
            
        }

        public interface IRequest
        {
            string GetInquiry();
        }
        public abstract class Request : IRequest
        {
            private string inquiry;

            public Request()
            {

            }

            public string GetInquiry()
            {
                return inquiry;
            }


        }

        public interface INetwork
        {
            void AddClient(Client _client);
            void AddServer(Server _server);
            void SendRequestFrom(Client _client, Server _server);
        }
        public abstract class Network
        {
            public List<Client> clientList;
            public List<Server> serverList;

            public Network()
            {

            }
        }
    }
}
