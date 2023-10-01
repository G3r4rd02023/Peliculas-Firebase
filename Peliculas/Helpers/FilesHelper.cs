using Firebase.Auth;
using Firebase.Storage;

namespace Peliculas.Helpers
{
    public class FilesHelper : IFilesHelper
    {
        public async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            //Este código se utiliza para autenticar un usuario en Firebase utilizando
            //su dirección de correo electrónico y contraseña, y luego subir un archivo a Firebase Storage. 

            //email y clave contienen la dirección de correo electrónico y la contraseña del usuario
            //que se utilizarán para iniciar sesión en Firebase.	
            string email = "tecnologershn@gmail.com";
            string clave = "Tecno123.";
            //ruta contiene la dirección del almacenamiento de Firebase donde se desea cargar el archivo.		
            string ruta = "peliculasdemo.appspot.com";
            //api_key es la clave de API necesaria para autenticarse con Firebase.
            string api_key = "AIzaSyChhPnzaVrpRraIo1JO5esK_mzo7gb4Ruk";


            //Se crea una instancia de FirebaseAuthProvider utilizando la clave de API proporcionada.
            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            //Se utiliza auth.SignInWithEmailAndPasswordAsync(email, clave) para autenticar al usuario
            //utilizando su dirección de correo electrónico y contraseña.
            //El resultado de esta autenticación se almacena en la variable a.
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            //Se crea una instancia de FirebaseStorage para interactuar con Firebase Storage.
            //Se configura con la ruta de almacenamiento y se proporciona un token de autenticación
            //en AuthTokenAsyncFactory, que se obtiene del objeto de autenticación a.
            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                //Se especifica una ubicación dentro del almacenamiento de Firebase donde se desea cargar el archivo.
                //En este caso, se utiliza la ruta "Fotos_Peliculas" y el nombre del archivo
                //se espera que esté en la variable nombre.
                .Child("Fotos_Perfil")
                .Child(nombre)

                //Se utiliza PutAsync para cargar el archivo especificado (archivo) a la ubicación de
                //Firebase Storage que se ha configurado. También se utiliza un token de cancelación para
                //permitir la cancelación de la operación si es necesario.
                .PutAsync(archivo, cancellation.Token);


            //Se espera a que se complete la carga del archivo y se almacena la URL de descarga en la variable downloadURL.
            var downloadURL = await task;

            //Finalmente, la URL de descarga se devuelve como resultado de la función.
            return downloadURL;

        }
    }
}
