import React from 'react';
import INavItemProps from "./INavItemProps";
import './NavItem.scss';

function NavItem(props: INavItemProps) {
    return(
        <>
            <div className="nav-item-text">{props.text}</div>
            <div className="nav-item-image">{props.image}</div>
        </>
    );
}

export default NavItem;