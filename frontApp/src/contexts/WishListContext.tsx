import React, { createContext, useState, useEffect, ReactNode } from 'react';

interface Product {
  id: number;
  title: string;
  price: number;
  description: string;
}

interface WishListContextType {
  wishList: Product[];
  addToWishList: (product: Product) => void;
  removeFromWishList: (id: number) => void;
}

export const WishListContext = createContext<WishListContextType | undefined>(undefined);

const WishListProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [wishList, setWishList] = useState<Product[]>([]);

  // Cargar productos deseados desde localStorage al iniciar la aplicación
  useEffect(() => {
    const storedWishList = localStorage.getItem('wishList');
    if (storedWishList) {
      setWishList(JSON.parse(storedWishList)); // Convertimos la cadena JSON a un arreglo
    }
  }, []);

  // Guardar productos deseados en localStorage cada vez que la lista cambie
  useEffect(() => {
    localStorage.setItem('wishList', JSON.stringify(wishList)); // Convertimos el arreglo a una cadena JSON
  }, [wishList]);

  // Función para agregar productos a la lista de deseados
  const addToWishList = (product: Product) => {
    if (!wishList.find(p => p.id === product.id)) {
      setWishList([...wishList, product]);
    }
  };

  // Función para eliminar productos de la lista de deseados
  const removeFromWishList = (id: number) => {
    setWishList(wishList.filter(product => product.id !== id));
  };

  return (
    <WishListContext.Provider value={{ wishList, addToWishList, removeFromWishList }}>
      {children}
    </WishListContext.Provider>
  );
};

export default WishListProvider;
