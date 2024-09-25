import React, { useContext, useState } from 'react';
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent, IonButton, IonCard, IonCardHeader, IonCardTitle, IonCardContent } from '@ionic/react';
import { WishListContext } from '../../contexts/WishListContext';
import { useHistory } from 'react-router';

const WishListPage: React.FC = () => {
    const { wishList, removeFromWishList } = useContext(WishListContext)!;
    const [sortCriteria, setSortCriteria] = useState<string>('name');
    const history = useHistory();
  
    const sortedWishList = [...wishList].sort((a, b) => {
      if (sortCriteria === 'name') {
        return a.title.localeCompare(b.title);
      } else if (sortCriteria === 'price') {
        return a.price - b.price;
      }
      return 0;
    });

    const productsPage = () => {
        history.push('/products');
    };
  
    return (
      <IonPage>
        <IonHeader>
          <IonToolbar>
            <IonTitle>Wish List</IonTitle>
          </IonToolbar>
        </IonHeader>
        <IonContent>
          <IonButton onClick={() => setSortCriteria('name')}>Ordenar por nombre</IonButton>
          <IonButton onClick={() => setSortCriteria('price')}>Ordenar por precio</IonButton>
          {sortedWishList.map(product => (
            <IonCard key={product.id}>
              <IonCardHeader>
                <IonCardTitle>{product.title}</IonCardTitle>
              </IonCardHeader>
              <IonCardContent>
                <p>Descripción: {product.description}</p>
                <p>Price: ${product.price}</p>
              </IonCardContent>
              <IonButton onClick={() => removeFromWishList(product.id)}>Eliminar de la lista</IonButton>
            </IonCard>
          ))}
        </IonContent>
        <IonButton expand="block" onClick={productsPage} className="ion-margin-top">
          Lista de productos
        </IonButton>
      </IonPage>
    );
  };

export default WishListPage;
