using System;
using System.Collections.Generic;
 
 
// ============================================================================
// CLASSE MÈRE : Produit
// ============================================================================
//
// Cette classe contient les informations communes à tous les produits.
//
// Notions utilisées :
// - encapsulation
// - readonly
// - propriétés
// - constructeur
// - static
// - héritage
// - virtual / override
// - polymorphisme
// ============================================================================
 
public class Produit
{
    // ========================================================================
    // CHAMPS PRIVÉS
    // ========================================================================
 
    // CODE est l'identifiant du produit.
    //
    // readonly signifie :
    // - on peut lui donner une valeur lors de sa déclaration
    //   ou dans le constructeur;
    // - une fois l'objet construit, cette valeur ne peut plus changer.
    //
    // C'est logique pour un identifiant.
    private readonly string code;
 
    private string nom = string.Empty;
    private double prix;
    private int quantite;
 
 
    // ========================================================================
    // MEMBRE STATIC
    // ========================================================================
 
    // Un seul compteur partagé par Produit
    // et toutes ses sous-classes.
    public static int NbProduits { get; private set; }
 
 
    // ========================================================================
    // CONSTRUCTEUR
    // ========================================================================
 
    public Produit(
        string code,
        string nom,
        double prix,
        int quantite
    )
    {
        // --------------------------------------------------------------------
        // Validation du CODE
        // --------------------------------------------------------------------
        //
        // Comme code est readonly, on valide puis on affecte
        // directement le champ dans le constructeur.
 
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException(
                "Le code du produit ne peut pas être vide."
            );
        }
 
        this.code = code;
 
 
        // --------------------------------------------------------------------
        // Autres propriétés
        // --------------------------------------------------------------------
        //
        // Nom, Prix et Quantite peuvent changer plus tard.
        // On utilise donc leurs propriétés pour bénéficier
        // des validations.
 
        Nom = nom;
        Prix = prix;
        Quantite = quantite;
 
 
        NbProduits++;
    }
 
 
    // ========================================================================
    // PROPRIÉTÉ CODE - LECTURE SEULE
    // ========================================================================
 
    public string Code
    {
        get
        {
            return code;
        }
    }
 
    /*
     * Donc :
     *
     * Console.WriteLine(produit.Code);  // OK
     *
     * produit.Code = "E999";            // INTERDIT
     *
     * Le code est fixé lors de la création.
     */
 
 
    // ========================================================================
    // PROPRIÉTÉ NOM
    // ========================================================================
 
    public string Nom
    {
        get
        {
            return nom;
        }
 
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Le nom du produit ne peut pas être vide."
                );
            }
 
            nom = value;
        }
    }
 
 
    // ========================================================================
    // PROPRIÉTÉ PRIX
    // ========================================================================
 
    public double Prix
    {
        get
        {
            return prix;
        }
 
        set
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "Le prix ne peut pas être négatif."
                );
            }
 
            prix = value;
        }
    }
 
 
    // ========================================================================
    // PROPRIÉTÉ QUANTITÉ
    // ========================================================================
 
    public int Quantite
    {
        get
        {
            return quantite;
        }
 
        set
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "La quantité ne peut pas être négative."
                );
            }
 
            quantite = value;
        }
    }
 
 
    // ========================================================================
    // VALEUR DU STOCK
    // ========================================================================
 
    public double ValeurStock()
    {
        return Prix * Quantite;
    }
 
 
    // ========================================================================
    // AFFICHER - VIRTUAL
    // ========================================================================
 
    public virtual void Afficher()
    {
        Console.WriteLine($"Code            : {Code}");
        Console.WriteLine($"Nom             : {Nom}");
        Console.WriteLine($"Prix            : {Prix:F2} $");
        Console.WriteLine($"Quantité        : {Quantite}");
        Console.WriteLine(
            $"Valeur du stock : {ValeurStock():F2} $"
        );
    }
 
 
    // ========================================================================
    // TOSTRING
    // ========================================================================
 
    public override string ToString()
    {
        return $"{Code} - {Nom} - {Prix:F2} $ - quantité : {Quantite}";
    }
}
 
 
// ============================================================================
// SOUS-CLASSE : ProduitElectronique
// ============================================================================
 
public class ProduitElectronique : Produit
{
    private int garantieMois;
 
 
    public ProduitElectronique(
        string code,
        string nom,
        double prix,
        int quantite,
        int garantieMois
    )
        : base(code, nom, prix, quantite)
    {
        if (garantieMois < 0)
        {
            throw new ArgumentException(
                "La garantie ne peut pas être négative."
            );
        }
 
        this.garantieMois = garantieMois;
    }
 
 
    public int GarantieMois
    {
        get
        {
            return garantieMois;
        }
    }
 
 
    public override void Afficher()
    {
        // Partie commune.
        base.Afficher();
 
        // Partie spécifique.
        Console.WriteLine("Type            : Électronique");
        Console.WriteLine(
            $"Garantie        : {GarantieMois} mois"
        );
    }
}
 
 
// ============================================================================
// SOUS-CLASSE : ProduitAlimentaire
// ============================================================================
 
public class ProduitAlimentaire : Produit
{
    private DateTime expiration;
 
 
    public ProduitAlimentaire(
        string code,
        string nom,
        double prix,
        int quantite,
        DateTime expiration
    )
        : base(code, nom, prix, quantite)
    {
        this.expiration = expiration;
    }
 
 
    public DateTime Expiration
    {
        get
        {
            return expiration;
        }
    }
 
 
    public override void Afficher()
    {
        base.Afficher();
 
        Console.WriteLine("Type            : Alimentaire");
 
        Console.WriteLine(
            $"Expiration      : {Expiration:yyyy-MM-dd}"
        );
    }
}
 
 
// ============================================================================
// SOUS-CLASSE : ProduitLivre
// ============================================================================
 
public class ProduitLivre : Produit
{
    private string auteur = string.Empty;
 
 
    public ProduitLivre(
        string code,
        string nom,
        double prix,
        int quantite,
        string auteur
    )
        : base(code, nom, prix, quantite)
    {
        if (string.IsNullOrWhiteSpace(auteur))
        {
            throw new ArgumentException(
                "L'auteur ne peut pas être vide."
            );
        }
 
        this.auteur = auteur;
    }
 
 
    public string Auteur
    {
        get
        {
            return auteur;
        }
    }
 
 
    public override void Afficher()
    {
        base.Afficher();
 
        Console.WriteLine("Type            : Livre");
        Console.WriteLine($"Auteur          : {Auteur}");
    }
}
 
 
// ============================================================================
// CLASSE : GestionInventaire
// ============================================================================
//
// Cette classe utilise plusieurs collections.
//
// List<Produit>
//     -> ajouter et parcourir.
//
// Dictionary<string, Produit>
//     -> retrouver un Produit grâce à son Code.
//
// HashSet<string>
//     -> empêcher deux produits avec le même Code.
//
// Stack<Produit>
//     -> annuler le dernier ajout.
//
// Queue<Produit>
//     -> gérer une file de réapprovisionnement.
// ============================================================================
 
public class GestionInventaire
{
    // ========================================================================
    // LIST
    // ========================================================================
 
    private List<Produit> produits =
        new List<Produit>();
 
 
    // ========================================================================
    // DICTIONARY
    // ========================================================================
 
    private Dictionary<string, Produit> produitsParCode =
        new Dictionary<string, Produit>();
 
 
    // ========================================================================
    // HASHSET
    // ========================================================================
 
    private HashSet<string> codes =
        new HashSet<string>();
 
 
    // ========================================================================
    // STACK
    // ========================================================================
 
    private Stack<Produit> historiqueAjouts =
        new Stack<Produit>();
 
 
    // ========================================================================
    // QUEUE
    // ========================================================================
 
    private Queue<Produit> fileReapprovisionnement =
        new Queue<Produit>();
 
 
    // ========================================================================
    // PRODUITS - PARCOURS SEULEMENT
    // ========================================================================
 
    public IEnumerable<Produit> Produits
    {
        get
        {
            return produits;
        }
    }
 
 
    // ========================================================================
    // CODES - PARCOURS SEULEMENT
    // ========================================================================
 
    public IEnumerable<string> Codes
    {
        get
        {
            return codes;
        }
    }
 
 
    // ========================================================================
    // AJOUTER
    // ========================================================================
 
    public bool Ajouter(Produit produit)
    {
        /*
         * Étape 1 :
         *
         * HashSet vérifie si le code existe déjà.
         *
         * Add retourne :
         *
         * true  -> nouveau code
         * false -> doublon
         */
 
        bool codeNouveau =
            codes.Add(produit.Code);
 
 
        if (!codeNouveau)
        {
            return false;
        }
 
 
        // Étape 2 :
        // garder le Produit dans la List.
        produits.Add(produit);
 
 
        // Étape 3 :
        // créer l'association :
        //
        // code -> produit
 
        produitsParCode[produit.Code] =
            produit;
 
 
        // Étape 4 :
        // mémoriser le dernier ajout.
        historiqueAjouts.Push(produit);
 
 
        return true;
    }
 
 
    // ========================================================================
    // CHERCHER PAR CODE
    // ========================================================================
 
    public Produit? ChercherParCode(
        string code
    )
    {
        if (produitsParCode.ContainsKey(code))
        {
            return produitsParCode[code];
        }
 
        return null;
    }
 
 
    // ========================================================================
    // AFFICHER TOUS
    // ========================================================================
 
    public void AfficherTous()
    {
        foreach (Produit produit in produits)
        {
            Console.WriteLine();
 
            Console.WriteLine(
                "----------------------------------------"
            );
 
 
            // Polymorphisme :
            //
            // ProduitElectronique
            // ProduitAlimentaire
            // ProduitLivre
            //
            // ont chacun leur version de Afficher().
 
            produit.Afficher();
        }
    }
 
 
    // ========================================================================
    // FILTRER SELON LE STOCK
    // ========================================================================
 
    public void AfficherStockFaible(
        int seuil
    )
    {
        foreach (Produit produit in produits)
        {
            if (produit.Quantite <= seuil)
            {
                Console.WriteLine(produit);
            }
        }
    }
 
 
    // ========================================================================
    // STACK - ANNULER LE DERNIER AJOUT
    // ========================================================================
 
    public Produit? AnnulerDernierAjout()
    {
        if (historiqueAjouts.Count == 0)
        {
            return null;
        }
 
 
        // LIFO :
        //
        // Last In
        // First Out
 
        Produit produit =
            historiqueAjouts.Pop();
 
 
        // Il faut maintenir toutes les collections cohérentes.
 
        produits.Remove(produit);
 
        produitsParCode.Remove(
            produit.Code
        );
 
        codes.Remove(
            produit.Code
        );
 
 
        return produit;
    }
 
 
    // ========================================================================
    // QUEUE - AJOUTER À LA FILE
    // ========================================================================
 
    public bool AjouterAFileReapprovisionnement(
        string code
    )
    {
        Produit? produit =
            ChercherParCode(code);
 
 
        if (produit == null)
        {
            return false;
        }
 
 
        // Ajouter à la fin.
        fileReapprovisionnement.Enqueue(
            produit
        );
 
 
        return true;
    }
 
 
    // ========================================================================
    // QUEUE - TRAITER LE PREMIER
    // ========================================================================
 
    public Produit? TraiterProchainReapprovisionnement()
    {
        if (fileReapprovisionnement.Count == 0)
        {
            return null;
        }
 
 
        // FIFO :
        //
        // First In
        // First Out
 
        return fileReapprovisionnement.Dequeue();
    }
}
 
 
// ============================================================================
// PROGRAMME PRINCIPAL
// ============================================================================
 
public class Program
{
    public static void Main(string[] args)
    {
        Titre(
            "LABO - COLLECTIONS ET GÉNÉRIQUES"
        );
 
 
        GestionInventaire inventaire =
            new GestionInventaire();
 
 
        // ====================================================================
        // TEST 1 - CRÉATION DES PRODUITS
        // ====================================================================
 
        Titre(
            "TEST 1 - Création des produits"
        );
 
 
        ProduitElectronique ecouteurs =
            new ProduitElectronique(
                "E001",
                "Écouteurs",
                79.99,
                8,
                24
            );
 
 
        ProduitAlimentaire yogourt =
            new ProduitAlimentaire(
                "A001",
                "Yogourt",
                3.99,
                3,
                new DateTime(
                    2026,
                    9,
                    15
                )
            );
 
 
        ProduitLivre livre =
            new ProduitLivre(
                "L001",
                "Clean Code",
                49.95,
                2,
                "Robert C. Martin"
            );
 
 
        ProduitElectronique clavier =
            new ProduitElectronique(
                "E002",
                "Clavier",
                129.99,
                12,
                36
            );
 
 
        Console.WriteLine(
            $"Nombre d'objets Produit créés : {Produit.NbProduits}"
        );
 
 
        // ====================================================================
        // TEST 2 - READONLY
        // ====================================================================
 
        Titre(
            "TEST 2 - Code readonly"
        );
 
 
        Console.WriteLine(
            $"Code des écouteurs : {ecouteurs.Code}"
        );
 
 
        /*
         * Cette ligne serait INTERDITE :
         *
         * ecouteurs.Code = "E999";
         *
         * Pourquoi ?
         *
         * Code ne possède pas de set
         * et le champ code est readonly.
         *
         * L'identifiant reste donc stable.
         */
 
 
        // ====================================================================
        // TEST 3 - AJOUTER LES PRODUITS
        // ====================================================================
 
        Titre(
            "TEST 3 - Ajouter les produits"
        );
 
 
        AjouterEtAfficherResultat(
            inventaire,
            ecouteurs
        );
 
 
        AjouterEtAfficherResultat(
            inventaire,
            yogourt
        );
 
 
        AjouterEtAfficherResultat(
            inventaire,
            livre
        );
 
 
        AjouterEtAfficherResultat(
            inventaire,
            clavier
        );
 
 
        // ====================================================================
        // TEST 4 - HASHSET : DOUBLON
        // ====================================================================
 
        Titre(
            "TEST 4 - Refuser un doublon"
        );
 
 
        bool ajouteEncore =
            inventaire.Ajouter(
                ecouteurs
            );
 
 
        if (ajouteEncore)
        {
            Console.WriteLine(
                "Produit ajouté."
            );
        }
        else
        {
            Console.WriteLine(
                "Ajout refusé : E001 existe déjà."
            );
        }
 
 
        // ====================================================================
        // TEST 5 - LIST + FOREACH + POLYMORPHISME
        // ====================================================================
 
        Titre(
            "TEST 5 - Parcourir la List"
        );
 
 
        inventaire.AfficherTous();
 
 
        // ====================================================================
        // TEST 6 - DICTIONARY
        // ====================================================================
 
        Titre(
            "TEST 6 - Recherche avec Dictionary"
        );
 
 
        Produit? trouve =
            inventaire.ChercherParCode(
                "A001"
            );
 
 
        if (trouve != null)
        {
            Console.WriteLine(
                "Produit trouvé :"
            );
 
            Console.WriteLine(
                trouve
            );
        }
        else
        {
            Console.WriteLine(
                "Produit absent."
            );
        }
 
 
        // ====================================================================
        // TEST 7 - CODE ABSENT
        // ====================================================================
 
        Titre(
            "TEST 7 - Code absent"
        );
 
 
        Produit? absent =
            inventaire.ChercherParCode(
                "Z999"
            );
 
 
        if (absent == null)
        {
            Console.WriteLine(
                "Z999 n'existe pas."
            );
        }
 
 
        // ====================================================================
        // TEST 8 - FILTRE
        // ====================================================================
 
        Titre(
            "TEST 8 - Stock faible"
        );
 
 
        Console.WriteLine(
            "Produits avec quantité <= 3 :"
        );
 
 
        inventaire.AfficherStockFaible(
            3
        );
 
 
        // ====================================================================
        // TEST 9 - MÉTHODE GÉNÉRIQUE
        // ====================================================================
 
        Titre(
            "TEST 9 - Méthode générique"
        );
 
 
        Console.WriteLine(
            "Produits :"
        );
 
 
        AfficherTous(
            inventaire.Produits
        );
 
 
        Console.WriteLine();
 
 
        Console.WriteLine(
            "Codes uniques :"
        );
 
 
        AfficherTous(
            inventaire.Codes
        );
 
 
        // ====================================================================
        // TEST 10 - STACK
        // ====================================================================
 
        Titre(
            "TEST 10 - Stack : annuler le dernier ajout"
        );
 
 
        Produit? produitAnnule =
            inventaire.AnnulerDernierAjout();
 
 
        if (produitAnnule != null)
        {
            Console.WriteLine(
                $"Produit annulé : {produitAnnule.Code} - {produitAnnule.Nom}"
            );
        }
 
 
        Console.WriteLine();
 
        Console.WriteLine(
            "Inventaire après annulation :"
        );
 
 
        AfficherTous(
            inventaire.Produits
        );
 
 
        // ====================================================================
        // TEST 11 - VÉRIFIER LE DICTIONARY
        // ====================================================================
 
        Titre(
            "TEST 11 - Collections cohérentes"
        );
 
 
        Produit? clavierApresAnnulation =
            inventaire.ChercherParCode(
                "E002"
            );
 
 
        if (clavierApresAnnulation == null)
        {
            Console.WriteLine(
                "E002 a aussi été retiré du Dictionary."
            );
        }
 
 
        // ====================================================================
        // TEST 12 - QUEUE
        // ====================================================================
 
        Titre(
            "TEST 12 - Queue : réapprovisionnement"
        );
 
 
        inventaire.AjouterAFileReapprovisionnement(
            "A001"
        );
 
        inventaire.AjouterAFileReapprovisionnement(
            "L001"
        );
 
        inventaire.AjouterAFileReapprovisionnement(
            "E001"
        );
 
 
        Produit? premier =
            inventaire.TraiterProchainReapprovisionnement();
 
 
        if (premier != null)
        {
            Console.WriteLine(
                $"Premier traité : {premier.Code} - {premier.Nom}"
            );
        }
 
 
        Produit? deuxieme =
            inventaire.TraiterProchainReapprovisionnement();
 
 
        if (deuxieme != null)
        {
            Console.WriteLine(
                $"Deuxième traité : {deuxieme.Code} - {deuxieme.Nom}"
            );
        }
 
 
        // ====================================================================
        // TEST 13 - AJOUTER UN NOUVEAU TYPE
        // ====================================================================
 
        Titre(
            "TEST 13 - Collections + polymorphisme"
        );
 
 
        ProduitLivre nouveauLivre =
            new ProduitLivre(
                "L002",
                "C# pour débutants",
                39.99,
                6,
                "Marie Tremblay"
            );
 
 
        inventaire.Ajouter(
            nouveauLivre
        );
 
 
        Console.WriteLine(
            "ProduitLivre ajouté sans créer une nouvelle List."
        );
 
 
        Console.WriteLine();
 
 
        inventaire.AfficherTous();
 
 
        // ====================================================================
        // FIN
        // ====================================================================
 
        Titre(
            "FIN DU LABO"
        );
    }
 
 
    // ========================================================================
    // MÉTHODE GÉNÉRIQUE
    // ========================================================================
 
    public static void AfficherTous<T>(
        IEnumerable<T> elements
    )
    {
        foreach (T element in elements)
        {
            Console.WriteLine(
                element
            );
        }
    }
 
 
    // ========================================================================
    // MÉTHODE D'AJOUT
    // ========================================================================
 
    public static void AjouterEtAfficherResultat(
        GestionInventaire inventaire,
        Produit produit
    )
    {
        bool ajoute =
            inventaire.Ajouter(
                produit
            );
 
 
        if (ajoute)
        {
            Console.WriteLine(
                $"Ajouté : {produit.Code} - {produit.Nom}"
            );
        }
        else
        {
            Console.WriteLine(
                $"Refusé : le code {produit.Code} existe déjà."
            );
        }
    }
 
 
    // ========================================================================
    // MÉTHODE D'AFFICHAGE DES TITRES
    // ========================================================================
 
    public static void Titre(
        string texte
    )
    {
        Console.WriteLine();
 
        Console.WriteLine(
            "============================================================"
        );
 
        Console.WriteLine(
            texte
        );
 
        Console.WriteLine(
            "============================================================"
        );
    }
}