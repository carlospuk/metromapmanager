using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatAttitude.Utilities.Metro.Mapping
{
    internal class AnnotationCollection
    {
        // Private
        HashSet<IMapAnnotation> annotations = new HashSet<IMapAnnotation>();

        // Events
        public event EventHandler<AnnotationCollectionChangedEventArgs> AnnotationsChanged;

        public AnnotationCollection()
        {
        }


        // Public Add/Remove methods
        public void addAnnotations(IEnumerable<IMapAnnotation> newAnnotations)
        {
            if (newAnnotations == null) return;

            bool anyNewAnnotations = false;

            HashSet<IMapAnnotation> annotationsAdded = new HashSet<IMapAnnotation>();

            foreach (IMapAnnotation annotation in newAnnotations)
            {
                bool isNew = annotations.Add(annotation);
                anyNewAnnotations |= isNew;

                if (isNew)
                    annotationsAdded.Add(annotation);
            }

            // Fire 'changed' event
            if (anyNewAnnotations)
                if (this.AnnotationsChanged != null)
                    this.AnnotationsChanged(this, new AnnotationCollectionChangedEventArgs(annotationsAdded, 
                        new HashSet<IMapAnnotation>() ));
        }
        public void removeAnnotations(IEnumerable<IMapAnnotation> annotationsToRemove)
        {
            if (annotationsToRemove == null) return;

            bool anyChanges = false;

            HashSet<IMapAnnotation> annotationsRemoved = new HashSet<IMapAnnotation>();

            foreach (IMapAnnotation annotation in annotationsToRemove)
            {
                bool itemExists = annotations.Remove(annotation);
                anyChanges &= itemExists;

                if (itemExists)
                    annotationsRemoved.Add(annotation);

            }

            // Fire 'changed' event
            if (anyChanges)
                if (this.AnnotationsChanged != null)
                    this.AnnotationsChanged(this, new AnnotationCollectionChangedEventArgs(
                        new HashSet<IMapAnnotation>(),
                        annotationsRemoved ));
        }
        public void setAnnotations(IEnumerable<IMapAnnotation> requiredAnnotations)
        {
            if (requiredAnnotations == null) return;

            List<IMapAnnotation> annotationsToRemove = annotations.Except(requiredAnnotations).ToList();
            List<IMapAnnotation> annotationsToAdd = requiredAnnotations.Except(annotations).ToList();

            if ((annotationsToAdd.Count() > 0) || (annotationsToRemove.Count() > 0))
            {
                foreach (IMapAnnotation annotation in annotationsToAdd)
                    annotations.Add(annotation);

                for (int i = 0; i < (annotationsToRemove.Count()); i++)
                {
                    annotations.Remove(annotationsToRemove[i]);
                }
                

                if (this.AnnotationsChanged != null)
                    this.AnnotationsChanged(this, new AnnotationCollectionChangedEventArgs(
                        annotationsToAdd,
                        annotationsToRemove));
            }


        }
    }

    internal class AnnotationCollectionChangedEventArgs : EventArgs
    {
        public IEnumerable<IMapAnnotation> annotationsAdded { get; set; }
        public IEnumerable<IMapAnnotation> annotationsRemoved { get; set; }

        internal AnnotationCollectionChangedEventArgs(
            IEnumerable<IMapAnnotation> annotationsAdded,
            IEnumerable<IMapAnnotation> annotationsRemoved)
        {
            this.annotationsAdded = annotationsAdded;
            this.annotationsRemoved = annotationsRemoved;
        }

    }
}


