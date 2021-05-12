using DoktoraSor.BusinessLayer.Abstract;
using DoktoraSor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoktoraSor.BusinessLayer
{
    public class CategoryManager: ManagerBase<Category>
    {
      //public override int Delete(Category cetagory)
      //  {
      //      CommentManager commentManager = new CommentManager();
      //      LikedManager likedManager = new LikedManager();
      //      NoteManager noteManager = new NoteManager(); //Kategori ile ilişkili notlarında silinmesi gerekir.
      //      foreach (Note note in cetagory.Notes.ToList())
      //      {
      //          //Note ile ilişkili like ve yorumları sil
      //          foreach (Liked like in note.Likes.ToList())
      //          {
      //              likedManager.Delete(like);
      //          }
      //          //Note ile ilişkili yotumların silinmesi

      //          foreach (Comment comment in note.Comments.ToList())
      //          {
      //              commentManager.Delete(comment);
      //          }

      //          noteManager.Delete(note); //notu sil
      //      }
      //      return base.Delete(cetagory);
      //  }
      
    }
}
