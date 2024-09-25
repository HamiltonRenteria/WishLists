import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import { IonButton, IonCard, IonCardContent, IonCardHeader, IonCardTitle, IonContent, IonPage, IonHeader, IonToolbar, IonTitle, IonSearchbar, IonGrid, IonRow, IonCol, IonImg, IonCardSubtitle } from '@ionic/react';
import { WishListContext } from '../../contexts/WishListContext';
import { useHistory } from 'react-router';
import { getProducts } from '../../services/api';

interface Product {
  id: number;
  title: string;
  price: number;
  description: string;
  images: string;
}

const ProductList: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [filteredProducts, setFilteredProducts] = useState<any[]>([]);
    const { addToWishList, removeFromWishList, wishList } = useContext(WishListContext)!;
    const [error, setError] = useState<string | null>(null);
    const history = useHistory();
  
    useEffect(() => {
        const fetchData = async () => {
            try {
              const data = await getProducts();
              setProducts(data);
              setFilteredProducts(data); // Inicialmente, mostrar todos los productos
            } catch (error) {
              console.error('Error al cargar productos:', error);
            }
        };
      
        fetchData();
    }, []);

    useEffect(() => {
        const results = products.filter(product =>
          product.title.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredProducts(results);
      }, [searchTerm, products]);

    const favoritePage = () => {
        history.push('/wishlist');
    };
  
    return (
      <IonPage>
        <IonHeader>
          <IonToolbar>
            <IonTitle>Lista de Productos</IonTitle>
          </IonToolbar>
        </IonHeader>
        <IonContent className='ion-padding'>
        <IonSearchbar
          value={searchTerm} 
          onIonInput={e => setSearchTerm(e.detail.value!)} 
          placeholder="Buscar productos..."
        />
          {error ? <p>{error}</p> : null}
          {products.map(product => (
            <IonGrid>
                <IonRow>
                    {filteredProducts.map((product) => (
                        <IonCol size="12" size-md="6" key={product.id}>
                        <IonCard>
                            <IonImg src={product.images[0]} alt={product.title} />
                            <IonCardHeader>
                                <IonCardSubtitle>${product.price}</IonCardSubtitle>
                                <IonCardTitle>{product.title}</IonCardTitle>
                            </IonCardHeader>
                            <IonCardContent>
                                <p>{product.description.substring(0, 100)}...</p>
                                <p></p>
                                <IonButton
                                onClick={() => addToWishList(product)}
                                disabled={wishList.some(p => p.id === product.id)}
                                >
                                Añadir a favoritos
                                </IonButton>
                                <IonButton
                                onClick={() => removeFromWishList(product.id)}
                                disabled={!wishList.some(p => p.id === product.id)}
                                >
                                Eliminar de favoritos
                                </IonButton>
                            </IonCardContent>
                        </IonCard>
                    </IonCol>
                    ))}
                </IonRow>
            </IonGrid>
          ))}
        </IonContent>
        <IonButton 
            expand="block" 
            onClick={favoritePage} 
            disabled={wishList.length == 0}
            className="ion-margin-top">
          Lista de favoritos
        </IonButton>
      </IonPage>
    );
  };

export default ProductList;
