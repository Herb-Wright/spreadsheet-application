// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

// AUTHOR: Herb Wright (Skeleton implementation by Joe Zachary)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            dependents = new Dictionary<string, HashSet<string>>();
            dependees = new Dictionary<string, HashSet<string>>();
            Size = 0;
        }

        /// <summary>
        /// Dictionary of the dependents of each string
        /// </summary>
        private Dictionary<string, HashSet<string>> dependents;
        /// <summary>
        /// Dictionary of the dependees of each string
        /// </summary>
        private Dictionary<string, HashSet<string>> dependees;


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size { get; private set; }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get { return dependents.ContainsKey(s) ? dependees[s].Count : 0; }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            return dependents.ContainsKey(s) ? dependents[s].Count > 0 : false;
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            return dependents.ContainsKey(s) ? dependees[s].Count > 0 : false;
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            return dependents.ContainsKey(s) ? new HashSet<string>(dependents[s]) : new HashSet<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            return dependents.ContainsKey(s) ? new HashSet<string>(dependees[s]) : new HashSet<string>();
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            AddHelper(s);
            AddHelper(t);
            if(dependents[s].Add(t) && dependees[t].Add(s)) { Size++; }

        }
        /// <summary>
        /// Helper method that checks if s is in the Dictionaries, if not,
        /// it adds s to them.
        /// </summary>
        /// <param name="s">the string to maybe be added</param>
        private void AddHelper(string s)
        {
            if (!dependents.ContainsKey(s)) { 
                dependents[s] = new HashSet<string>();
                dependees[s] = new HashSet<string>();
            }
        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            AddHelper(s);
            AddHelper(t);
            if(dependents[s].Remove(t) && dependees[t].Remove(s)) { Size--; }
            RemoveHelper(s);
            RemoveHelper(t);
        }
        /// <summary>
        /// Helper method that checks to see if s has any dependents or dependees, 
        /// if not, it removes it from the Dictionaries.
        /// </summary>
        /// <param name="s">The string to maybe be removed</param>
        private void RemoveHelper(string s)
        {
            if(dependents.ContainsKey(s) && dependents[s].Count == 0 && dependees[s].Count == 0)
            {
                dependents.Remove(s);
                dependees.Remove(s);
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            AddHelper(s);
            foreach (string t in new HashSet<string>(GetDependents(s)))
            {
                RemoveDependency(s, t);
            }
            foreach (string t in newDependents)
            {
                AddDependency(s, t);
            };
            RemoveHelper(s);
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            AddHelper(s);
            foreach (string t in new HashSet<string>(GetDependees(s)))
            {
                RemoveDependency(t, s);
            }
            foreach (string t in newDependees)
            {
                AddDependency(t, s);
            };
            RemoveHelper(s);
        }

    }

}