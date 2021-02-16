[[_TOC_]]


Fenix-Versions es el programa encargado de ajustar las versiones de los paquetes que se vayan creando, además de actualizar los paquetes que dependen de él.

El repositorio principal de las versiones, en esta primera versión usará la master, mientras en el caso de los dependant, le asignará las que corresponda de acuerdo a la actualización.

Existen dos etapas, la primera es la actualización del paquete y la segunda es la actualización de las dependencias.

El programa tiene como entrada:

1. el repositorio de github.
2. la rama que está modificando.
3. build (opcional si es la rama master) de la ejecución.
4. el token que será usado para el acceso al github.
5. el nombre del paquete que está actualizando.
6. enumeración que identifica si es nuget o npm.
7. releaseDependant, esto aplica a la rama develop u features


El programa tiene dos etapas.

1. Generación de la versión.
2. Actualización de dependencias.

# Etapa 1 "Generación de la versión"

## Resumen

La etapa 1 actualizará la versión del paquete a la continuación de la parte de la versión semántica (major, minor y patch). el versionamiento de este será de acuerdo al tipo de rama, a continuación un ejemplo, basado en gitflow.


![git-flow](https://images.trifenix.io/git-model.png)

Como se puede apreciar en la imágen, la generación del minor y el patch dependen de la rama a actualizar. 

# Pasos

1. Creación de una estructura que permite tener el listado de paquetes de trifenix connect, tanto npm, como nuget y en cada uno, tener el listado de paquetes que dependen de él.

2. La estructura inicial estará en el mismo programa en duro, con el fin de generar una primera estructura que se irá actualizando conforme se vaya ejecutando el programa.

3. El programa guardará esta estructura en un github, donde actualizará el modelo según el incremento del patch o minor de acuerdo a la rama.

4. Este json generado de la estructura puede ser cambiado manualmente o eliminado para volver a ser recreado desde la estructura en duro del programa.

5. Cualquier desarrollador con acceso al repositorio, puede modificar manualmente la versión de cada uno de los paquetes.

6. Para el caso de modificación del major se hará exclusivamente desde github o con la rama release.


7. La estructura debe incorporar la url del github de cada uno.

8. El token de github se inyectará en la url de los githubs, con el fin de realizar operaciones. 

9. Con el token y la url de github realizará las operaciones CRUD sobre el json que representa la estructura.

10. El comando que será ejecutado para conseguir la nueva versión de un paquete retornará la nueva versión semántica a ser incluida en el paquete npm o nuget.

11. Todas las urls de github deben ser http(s) (debe ser validado).



# Entidades






## Estructura VersionStructure

Un version structure es la estructura que tiene un documento git que representa un paquete, en esta existirán las siguientes propiedades.


| Propiedades | Descripción |
|--|--|
| PackageName | Nombre del paquete, será usado para formar el nombre de archivo, conjunto con la enumeración que indica el tipo de paquete (npm, nuget). |
| PackageType | Tipo de paquete (npm, nuget). |
| github-http | Repositorio de github en http |
| github-ssh | Repositorio de github en ssh |
| latest | Colección de CommitVersion que almacena  por cada rama y su versión actualizada. |
| versions | Colección de todas las versiones generadas en el archivo de las distintas ramas |
| dependency | Colección de Dependency, que reflejan las dependencias de este paquete |

## CommitVersion
Commit version es una estructura que almacena los detalles de una versión, las propiedades son:

| Propiedades | Descripción |
|--|--|
| branch| Rama a la que pertenece el registro. |
| minor| Sección de la versión semántica. |
| patch| Sección de la versión semántica. |
| pre-release| cadena de texto que representa la palabra clave usada por la rama (preview, preview-release, alpha, rc, etc). |
| preview| Número entero que representa las ejecuciones de la versión. |
| dependantRelease| Campo booleano que indica que la rama es posterior a la develop, por tanto se le debe añadir la palabra release. |
| is-feature| si es feature, indica el tipo de rama que tendrá que actualizar en los dependants. |
| ToString()| Sobrescribe el método por defecto y forma la versión (1.0.1.preview-release.23). |




## Dependency
Unidad que representa una dependencia dentro de un paquete, con este listado, se actualizarán todos los paquetes que dependan de él.

Un dependency tendría las siguientes propiedades:


| Propiedades | Descripción |
|--|--|
| packageName | Nombre del paquete de la dependencia. |
| github-http| repositorio de github en http. |
| github-ssh | repositorio en github de ssh. |
| pathVersion | ruta del csproj u package.json |


El tipo de paquete se asume por el ente superior (VersionStructure).





# VersionSpec

## Descripción

VersionSpec será el componente principal, en el se ejecutarán todos los procesos principales, a continuación un listado.


# Métodos

## **GetVersionStructure**

### **Parámetros**

| Parámetro| Descripción |
|--|--|
| **gitOperation** |  tendrá un método asíncrono llamado GetElement, la cual entregará el objeto tipado, obteniendo como parametro el path del json que queremos obtener. |
| **versionStructure** | Estructura de dependencias por defecto, esto puede ser opcional, pero se debe tener la certeza que existe la estructura (CustomException). |
| **packageName** | El nombre del paquete será usado para identificar el path del archivo github (package-name.json). |





### **Retorno**
El método retornará la estructura VersionStructure de un paquete en particular, con los detalles de este y las dependencias de él.

### **Descripción**.
Este método obtendrá desde la interface IGitOperations el string con el que serializará la estructura DependencyStructure, en caso de que no exista la ruta (IGitOperations.GetStructure == null) usará los valores por defecto, sino se indica la estructura por defecto, lanzará excepción. 

### **Como se probará**

Se harán siguientes tests:

1. string mal formado, retorna excepción.
2. string vacio/nulo, usa los valores por defecto, validar por defecto (hash).
3. validar que si no existe en github y no se ingresa la estructura inicial devuelve excepción.
4. puede que exista validación de la estructura ?


## SetMainVersion
### Parámetros


| Parámetro| Descripción |
|--|--|
|branch| la rama determinará como obtendrá la versión, de acuerdo a la rama tendrá o no la palabra clave alpha, beta, RC u hotfix, en el caso de master no llevará nada. Además en ramas que no son la master incorporará el build en el commit y en la versión. |
|build| Build actual, el que permitirá conocer cual fué el build de la ejecución del yml, este será usado para el commit y para la nueva versión. |
|versionStructure| versionStructure a actualizar. |

### Retorno

Retorna un VersionStructure, manteniendo los valores, asignando una colección y actualizando el latest. 

Considera un flujo que determinará la creación de la versión del paquete, guardando el build, 
el commit, el tag y la palabra del prelease (Alpha, Beta, RC [Release Candidate]).

Las palabras claves convendrán de acuerdo a la rama.

preview = develop
preview-release = cuando se indica que la rama develop es dependiente del release.
alpha.(feature name or branch) = Features u otras ramas
hotfix = hotfix
RC = Release

![semantic](https://images.trifenix.io/semantic.png)



### Descripción
El objetivo del método es asignar la versión a una estructura, de acuerdo al nombre de la rama.

**Debe considerar lo siguiente**

a. Si la rama es **master**, a considerar.

1. No considera el build, ni tampoco la palabra clave. 

2. Si el release es menor a la master con el patch + 1, se mantendrá con este. si es mayor usará el release.


3. Si no existe versión en la estructura, lanzará error, dado que, al menos la master, debe tener una versión inicial.


b. Si la rama fuera develop, a considerar:

1. si el release es mayor al de la master y dependantRelease es verdadero usará preview-release, si no usará preview.

2. No se sumará en el patch, sino en un nuevo número incremental que se llama preview, este número al generar la versión estará al final de la palabra.

c. Si la rama fuera Release, a considerar:

1. tomaría la versión de acuerdo a la rama, donde obligatoriamente las ramas de releases serían release/X.X.X donde x sería el número de cada sección de una versión semántica, esto debe ser validado.

2. si el release es menor a la master, lanzaría un warning y no lo consideraría, solo entregaría la versión del release obtenido de la rama.

3. El release determina el cambio de versión semántica.

4. la palabra usada para esta rama es RC (Release Candidate).

5. sumaría en el preview, no aplicaría patch.

  
d. Si fuera otra rama, se consideraría.

1. se usaría la palabra alpha.

2. posterior a la palabra alpha, iría el nombre de la feature sacada desde el nombre de la rama (feature/nombre-de-la-feature), si se detecta que la rama no obedece a esa estructura, ocupará el mismo nombre de esta.

3. Sumaría en el preview, no aplicaría patch.


e. En el caso de las ramas que no sean la master, incorporará el build al final.


De acuerdo a la rama también actualizará las últimas versiones de VersionStructure.

### Como se prueba
De acuerdo a la rama y los datos del versionStructure con valores históricos, comprobar el nuevo commitVersion que se asignó, también el latest. 


## SetVersion

### Parámetros

| Parámetro| Descripción |
|--|--|
|branch| la rama determinará como obtendrá la versión, de acuerdo a la rama tendrá o no la palabra clave alpha, beta, RC u hotfix, en el caso de master no llevará nada. Además en ramas que no son la master incorporará el build en el commit y en la versión. |
|build| la rama determinará como obtendrá la versión, de acuerdo a la rama tendrá o no la palabra clave alpha, beta, RC u hotfix, Build actual, el que permitirá conocer cual fué el build de la ejecución del yml, este será usado para el commit y para la nueva versión. |
|token| Token necesario para operar sobre github. |
|packageName| Nombre del paquete que será actualizado. |
|packageType| Enumeración que representa si es nuget o npm. |
|releaseDependant| Campo que determina, si la rama es post release, si es post-release, se le asigna release a la palabra clave de release. |


### Retorno
String con la versión registrada en github.

### Descripción

Pasos


1. De acuerdo al packageName y el packageType, se hará la ruta relativa (raíz del repositorio github).
2. Usará GetVersionStructure, para obtener desde github la estructura del archivo del paquete.
3. Usará SetMainVersion, para que las versiones sean actualizadas.
4. Usará SaveFile de github para guardar el archivo con las versiones actualizadas.



# GithubRepo
El objetivo de este repositorio es generar y guardar archivos en base a un json, un crud para github, para esto debe tener los métodos que permitirán al menos agregar y actualizar.

Lo ideal sería que reutilizara un repositorio de trifenix connect, pero para la primera versión lo dejaremos como trifenix.git.

Este proyecto sería una librería independiente y utilizaría un generics para serializar y deserializar a string el contenido del archivo.

## !Importante

Este componente no será testeado, por tanto debe tener bien determinados los controles de excepción.

## Constructor
El constructor inicializará el repositorio, creando las variables de ambiente para la ejecución de las operaciones.

### Parámetros


| Parámetro| Descripción |
|--|--|
|github| Github donde operará. |
|token| Token que utilizará para poder hacer push sobre la rama. |
|email| email que será usado. |
|userName| userName que será usado. |
|T98 | Generics para serializar y deserializar el contenido. |


## SaveFile
### Parámetros
| Parámetro| Descripción |
|--|--|
|path| Path del objeto que queremos persistir, incluida la extensión. |
|branch| Rama donde se hará el commit. |
|commit| string con el commit a usar (será usado como tag). |
|T| objeto que queremos persistir. |

### Retorno
string con el commit si logró con exito la modificación.

### Descripción
Save File hará uso de commit para registrar o actualizar un archivo en el github.

## Commit

### Parámetros

| Parámetro| Descripción |
|--|--|
|branch| Rama donde se hará el commit.|
|Operations| Diccionario de acciones, Campo que determina las acciones que se realizarán por commit.|

### Parámetros de operations
| Parámetro| Descripción |
|--|--|
|key| el key será el commit, como string. Para este caso estamos asumiendo que en el commit irá la versión, por tanto también registraremos el commit como tag.|
|value| el value es una acción que hace una operación en los archivos del repositorio que se encuentra git y por tanto, queda registrado su cambio.|


### Descripción
Acción que registra los cambios realizados por las operaciones en los archivos del repositorio de github actual.

Por cada registro, realiza un commit con el key de las acciones.


## GetElement

### Parámetros
| Parámetro| Descripción |
|--|--|
|path| ruta del archivo a obtener, el que será serializado al tipo de retorno (excepción).|
|force| Obliga a realizar un nuevo clone para obtener el archivo.|


### Retorno
Retorna el objeto serializado, de acuerdo a la ruta especificada.

### Descripción
Método que retorna el elemento indicado por el generics T, si no existe el archivo en la ruta especificada, clonará, lo mismo si se especifica el parámetro force == true, al generar un nuevo clone se realizará sobre una nueva carpeta temporal.

Al finalizar la operación de clone guardará en una variable de la clase (C#), la ruta de la carpeta temporal, de esta manera si a la próxima no es force podrá obtener el elemento directo de la ruta (carpeta temporal creada anteriormente).

## Clone
Ejecuta un nuevo clone, retornando la carpeta temporal donde la creó.


# Utiles

## SetGithubToken
Método que asigne el token y el repositorio para una url github válida.

### Como se prueba
Se asigna un valor de token y se asigna un resultado.

## GetRelease
Obtiene la versión semántica de un release, desde un nombre de una rama, si no encuentra la palabra release retorna null.

### Como se prueba
Se asigna un string como rama y obtiene la versión indicada en el string.

## GetFeatureName
Obtiene el nombre de un feature desde la rama, si no encuentra la palabra feature, retornará el nombre de la rama.

### Como se prueba
Se asigna un nombre de la rama, con y sin feature y que retorne el feature o el nombre de la rama.

## GetBranchDependant
Método que permitirá obtener la rama del dependant de acuerdo al commitVersion asignado. 

### Como se prueba
asignar commitVersions con valores y validar que la rama sea la correcta.

## GetPreReleaseLabel
Obtiene el preleaselabel de acuerdo a la rama y el campo releaseDependant

### Como se prueba
Se indica el nombre de la rama y el releaseDependant y se espera un string que coincida.

## GetFullPackageName
Usa el folder, el nombre del paquete y el tipo para obtener la ruta del archivo, incluida la extensión json.

### como se prueba
Comparando con un string difinido.



# Etapa 2 "Generación de dependencias"

En esta etapa ya habremos actualizado un paquete y es hora de actualizar sus dependencias, para esto veremos las dependencias del paquete actual y accederemos a cada github actualizando el paquete de la versión actualizada. Según la rama que haga la operación, se actualizará la rama de las versiones que dependan de esta, si no encuentra la rama, arrojará un warning y continuará con los otros paquetes (ejecución paralela).


A continuación los métodos, todos ellos serán dependientes de VersionSpec

## SetVersionToDependant

Se encargará de asignar la versión actualizada a todos los repositorios. 

### Parámetros
| Parámetro| Descripción |
|--|--|
|packageName| nombre del paquete a actualizar|
|branch| rama que deberá ser usada en los dependants.|
|releaseDependant| determina si es antes o después del release.|
|packageType| tipo de paquete.|
|token| token usado para actualizar los paquetes.|



### Descripción
Con el packageName y el PackageType, irá a buscar el versionStructure con los datos, con el branch devolverá el objeto commitVersion con la versión a actualizar. 

Del mismo objeto, tomará la colección de dependencies, con el que actualizará a la nueva versión.

Desde el commitVersion, podrá obtener la versión con el método toString.

esta versión será usada para actualizar los paquetes de los dependants.

Por cada dependant, clonará el proyecto.

De acuerdo al tipo de paquete (npm o nuget), tomará el archivo de configuración y asignará la versión del paquete. Para esto estará determinado SetPackageJsonNpmVersion y SetCsProjNugetVersion.

una vez actualizado el paquete, se usará como acción del método commit del HubRepo, donde indicará la versión y actualizará el archivo. 

Al finalizar el commit, hará un push con el token, usuario y correo configurado.


## Como se prueba
Se debe de hacer mocking de las dependencias a github y verificar que las llamadas a actualizar sean de acuerdo al paquete, la rama y el nombre de la versión, que esté almacenando el mocking.



## SetPackageJsonNpmVersion
De acuerdo a una ruta del package.json y la versión a asignar asignará el valor y retornará el string con la modificación, este string podrá ser persistido, para esto debe identificar la ubicación del paquete y cambiar la versión de esta.

### Como se prueba
Se creará un archivo json de origen y destino, al lograr transformar el objeto a la misma versión se logrará el resultado.


## SetCsProjNugetVersion
De acuerdo a una ruta del *.csproj y la versión a asignar, asignará el valor y retornará el string con la modificación, este string podrá ser persistido.

Debe identificar donde esta la versión del paquete y modificar su versión.


### Como se prueba
Debemos de incluir una ruta de un csproj y actualizar su versión, el método para el assert usará un archivo tanto con el resultado inicial y el esperado.





# Scaffolding

Debemos de generar un nuevo proyecto, el cual considere ser liberado en conjunto como paquete nuget. Para esto debemos de considerar las siguientes etapas.


## Etapa 1, Hello World
Considerar como primera versión un comando en nuget, que pueda ser instalado en windows y linux, solo desplegando el mensaje  que se le asigne a la variable 
para la generación de la versión se debe considerar gitflow.

### Version
0.0.1
Inicio del proyecto y la vinculación a la documentación.

### como se prueba
Exploración visual en nuget.org

## Etapa2, mensajes
De acuerdo a los comandos asignados, realizar pruebas con strings que representen los valores ingresados.

### Version 0.0.2
Pruebas de comandos 

### Como se prueba
Se asignan valores por defecto y se espera un resultado en el objeto que retorna los parámetros de entrada. Se debe hacer mocking del programa que usemos para [procesar los comandos](https://www.nuget.org/packages/CommandLineParser).

## Etapa3, LibGit2Sharp
Pruebas de plataforma con librería, con el fin de determinar si es posible realizar operaciones de carpeta en linux y windows.

### Version 0.0.3
Se concluye la factibilidad de la multiplataforma.


## Etapa 3, post tests

Una vez finalizado los tests, se harán pruebas de líneas de comandos, documentando la operación.

### Version 0.0.4
Documentación de ejecución de línea de comandos.

### Como se prueba
Documentación del Readme del programa.


## Etapa 4, CI/CD

En la integración y entrega continua se debe considerar lo siguiente:

1. Crear un check para modificación de la master, donde se puedan validar los tests, este check permitirá modificar la master en un pull request.
2. Crear un pipeline para cada rama, incluido hotfix y feature.
3. Asignar el repositorio privado trifenix solo para la master, asignar draft para las otras ramas.
4. En el caso de la master, debe publicar en nuget.org.
5. En el caso de las otras ramas, solo en el repositorio draft.
6. La rama master se hará modificación según pull request, usando el check y un autorizador.

## Etapa 5, asignación a yml.
A estas alturas y de acuerdo a las pruebas, el listado de archivos con los paquetes ya ha sido definido o al menos especificado el inicializador en el programa. 

De esta manera el programa podrá ser ejecutado desde los yml, asignando los parámetros según la rama o la condición en que el build es ejecutado. 

Es importante asignar una prueba de tipo echo, cosa de poder verificar el resultado, antes de la asignación en los comandos para asignar las versiones para npm o nuget.

### Como probar
Realizar una modificación que tenga dependencias que puedan ser ramificadas a otras y chequear la ejecución positiva en Azure Devops.


## Cierre de la versión.

Asignación de los tests y resultados a este documento.


# Roadmap

1. Asignación de información de Release desde el pull request en github.
















