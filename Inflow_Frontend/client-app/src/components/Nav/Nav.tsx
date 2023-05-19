import React, {JSX, useEffect, useState} from 'react';
import './Nav.scss'
import {LAYOUT_REF} from "../Layout";
import NavItem from './NavItem';
import INavItemConfig from "./INavItemConfig";

function Nav() {
    const navComponentClassNameText : string = "nav-component";
    const navComponentCollapsedClassNameText : string = "nav-component collapsed";

    const [toggleButtonText, setToggleButtonText] = useState("<");
    const [navComponentClassName, setNavComponentClassName] = useState("nav-component");

    /**
     * TODO: Add redux usage.
     */
    const [navItems, setNavItems] = useState<JSX.Element[]>([]);

    let navItemsConfig: INavItemConfig[];

    useEffect(() => {
        const sectionSchemas = getSectionSchemas();

        navItemsConfig = sectionSchemas.map((sectionSchema) => {
            return {
                ...sectionSchema,
                selected: false
            }
        });

        navItemsConfig[0].selected = true;

        let navItemsFromSectionSchemas = getNavItemsFromConfig(navItemsConfig);
        setNavItems(navItemsFromSectionSchemas);
    }, []);

    function getSectionSchemas() {
        const mockResponse = [
            {
                id: "1",
                name: "Nav item #1",
                image: "img"
            },
            {
                id: "2",
                name: "Nav item #2",
                image: "img"
            },
            {
                id: "3",
                name: "Nav item #3",
                image: "img"
            },
            {
                id: "4",
                name: "Nav item #4",
                image: "img"
            },
            {
                id: "5",
                name: "Nav item #5",
                image: "img"
            }
        ];

        return mockResponse;
    }

    function getNavItemsFromConfig(config: INavItemConfig[]) : JSX.Element[] {
        const navItems: JSX.Element[] = config.map((configItem: INavItemConfig) => {
            return(
                <div
                    id={`${configItem.id}`}
                    className={configItem.selected ? "nav-item selected" : "nav-item"}
                    onClick={(e: any) => onNavItemClick(e)}
                >
                    <NavItem text={configItem.name} image={configItem.image} />
                </div>
            );
        });

        return navItems;
    }

    function onNavItemClick(e: any) {
        navItemsConfig.forEach((navItemConfig: INavItemConfig, i: number, arr: any[]) => {
            if (navItemConfig.selected) {
                arr[i].selected = false
            }

            let target = e.target;

            if (navItemConfig.id === target.id) {
                arr[i].selected = true;
                return;
            }

            if (navItemConfig.id === target.parentElement.id) {
                arr[i].selected = true;
            }
        });

        let updatedNavItems = getNavItemsFromConfig(navItemsConfig);

        /*
         * TODO: Очень плохая реализация. Перерисовываются все NavItem'ы, что в будущем аукнется, но сейчам нет времени.
         * Необходимо перерисовывать только выбранный NavItem и выбранный ранее.
         */
        setNavItems(updatedNavItems);
    }

    function onToggleButtonClick() {
        const navClassName : string =
            navComponentClassName === navComponentClassNameText
                ? navComponentCollapsedClassNameText
                : navComponentClassNameText;

        setNavComponentClassName(navClassName);

        LAYOUT_REF.current.classList.toggle("nav-collapsed");

        const toggleText : string = toggleButtonText === "<" ? ">" : "<";
        setToggleButtonText(toggleText);
    }

    return(
        <div className={navComponentClassName}>
            <div className="inflow-label-container">
                <div className="inflow-label">Inflow</div>
            </div>
            <div className="nav-items-container">
                {navItems}
            </div>
            <div className="nav-controls">
                <div className="user-profile" />
                <div
                    className="toggle-button"
                    onClick={() => onToggleButtonClick()}
                >{toggleButtonText}</div>
            </div>
        </div>
    );
}

export default Nav;